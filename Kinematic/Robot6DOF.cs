using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
    public static class Robot6DOF
    {
        public static List<IJoint> Get6DOFRobot(params double[] link)
        {
            if (link.Count() != 6)
            {
                throw new ArgumentException("6 DoF Robot should have 6 links", "link");
            }
            RotationJoint th1 = new RotationJoint
            {
                Length = link[0],
                TransformAxis = TransformType.RotateZ
            };
            RotationJoint th2 = new RotationJoint
            {
                Length = link[1],
                TransformAxis = TransformType.RotateY
            };
            RotationJoint th3 = new RotationJoint
            {
                Length = link[2],
                TransformAxis = TransformType.RotateY
            };
            RotationJoint th4 = new RotationJoint
            {
                Length = link[3],
                TransformAxis = TransformType.RotateZ
            };
            RotationJoint th5 = new RotationJoint
            {
                Length = link[4],
                TransformAxis = TransformType.RotateY
            };
            RotationJoint th6 = new RotationJoint
            {
                Length = link[5],
                TransformAxis = TransformType.RotateZ
            };

            return new List<IJoint> { th1, th2, th3, th4, th5, th6 };

        }

        public static Vector3F2 Inverse6DOF(ref List<IJoint> robot, Transform3D transform)
        {
            double l1, l2, l3, l4;
            l1 = robot[0].Length;
            l2 = robot[1].Length;
            l3 = robot[2].Length + robot[3].Length;
            l4 = robot[4].Length + robot[5].Length;

            Vector3F vec6z = transform.RotationMat.Col[2];
            Vector3F posJ4end = transform.Position - l4 * vec6z;
            Vector3F2 result = new Vector3F2();
            {
                Vector3F tvec = posJ4end;
                double y = tvec * new Vector3F(0, 1, 0);
                double x = tvec * new Vector3F(1, 0, 0);
                result[0] = Math.Atan2(y,x);
                result[0] = result[0] + Vector3F.CompareEPS < 0 ? result[0] + Math.PI * 2 : result[0];
                robot[0].Transform = new Vector3F(0, 0, result[0]);
            }
            {
                Vector3F tvec = posJ4end - l1 * new Vector3F(0, 0, 1);
                double ll2 = l2 * l2 + l3 * l3;
                double csc = (tvec.Length * tvec.Length - ll2) / (2 * l2 * l3);
                csc = Math.Round(csc, 12);
                result[2] = Math.Acos(csc);
                robot[2].Transform = new Vector3F(0,result[2], 0);
            }
            {
                double A = Math.Sqrt(Math.Pow(l3 * Math.Sin(result[2]), 2) + Math.Pow(l3 * Math.Cos(result[2]) + l2, 2));
                double asc = (posJ4end.Z - l1) / A;
                asc = Math.Round(asc, 12);
                double t0 = Math.Asin(asc);
                double t1 = Math.Acos((l2 * l2 + A * A - l3 * l3) / (2 * l2 * A));
                result[1] = Math.PI / 2 - t0 - t1;
                robot[1].Transform = new Vector3F(0, result[1], 0);
            }
            Transform3D base_axis = new Transform3D(new Vector3F(0, 0, 0), new Vector3F(0, 0, 0));
            Transform3D tf3d = base_axis;
            for (int i = 0; i < 3; i++)
            {
                tf3d = robot[i].GetEndPointState(tf3d);
            }
            Vector3F vec3z = tf3d.RotationMat.Col[2];
            Vector3F vec3y = tf3d.RotationMat.Col[1];
            Vector3F vec3x = tf3d.RotationMat.Col[0];
            Vector3F endUnit = vec6z;
            result[4] = Math.Acos(vec6z * vec3z);
            robot[4].Transform = new Vector3F(0, result[4], 0);
            result[3] = Math.Atan2( vec6z * vec3y, vec6z * vec3x);
            result[3] = result[3] + Vector3F.CompareEPS < 0 ? result[3] + Math.PI * 2 : result[3];
            robot[3].Transform = new Vector3F(0, 0, result[3]);

            for (int i = 3; i < 5; i++)
            {
                tf3d = robot[i].GetEndPointState(tf3d);
            }

            Vector3F vec5y = tf3d.RotationMat.Col[1];
            Vector3F vec5x = tf3d.RotationMat.Col[0];
            Vector3F vec6x = transform.RotationMat.Col[0];

            result[5] = Math.Atan2(vec6x*vec5y,vec6x*vec5x);
            result[5] = result[5] + Vector3F.CompareEPS < 0 ? result[5] + Math.PI * 2 : result[5];
            robot[5].Transform = new Vector3F(0, 0, result[5]);
            return result;
        }
    }
}
