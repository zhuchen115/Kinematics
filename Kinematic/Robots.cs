using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
    public static class Robots3DOF
    {
        /// <summary>
        /// Get the joints of the cartesian 
        /// </summary>
        /// <param name="lx">basic length of x axis offset</param>
        /// <param name="ly">basic length of y axis offset</param>
        /// <param name="lz">basic length of z axis offset</param>
        /// <returns>joints list</returns>
        public static List<IJoint> GetCartesian(double lx, double ly, double lz)
        {
            TranslationJoint sx = new TranslationJoint
            {
                Length = lx,
                TransformAxis = TransformType.AxisX
            };

            TranslationJoint sy = new TranslationJoint
            {
                Length = ly,
                TransformAxis = TransformType.AxisY
            };

            TranslationJoint sz = new TranslationJoint
            {
                Length = lz,
                TransformAxis = TransformType.AxisZ
            };

            List<IJoint> joints = new List<IJoint>
            {
                sx,
                sy,
                sz
            };
            return joints;
        }

        /// <summary>
        /// Get the joints list of Cylindical Robot
        /// </summary>
        /// <param name="l1">z offset</param>
        /// <param name="l2">z offset</param>
        /// <param name="l3">radius offset</param>
        /// <returns></returns>
        public static List<IJoint> GetCylindical(double l1,double l2,double l3)
        {
            RotationJoint phi = new RotationJoint
            {
                Length = l1,
                TransformAxis = TransformType.RotateZ
            };

            TranslationJoint h = new TranslationJoint
            {
                Length = l2,
                TransformAxis = TransformType.AxisZ
            };

            // Just assume the initial state of this joint is pointing to Y axis
            TranslationJoint r = new TranslationJoint
            {
                Length = l3,
                TransformAxis = TransformType.AxisY
            };

            List<IJoint> joints = new List<IJoint>
            {
                phi,
                h,
                r
            };

            return joints;
        }

        /// <summary>
        /// Get a configuration of Spherical robot
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="l3"></param>
        /// <returns></returns>
        public static List<IJoint> GetSpherical(double l1, double l2, double l3)
        {
            RotationJoint phi = new RotationJoint
            {
                Length = l1,
                TransformAxis = TransformType.RotateZ
            };

            RotationJoint theta = new RotationJoint
            {
                Length = l2,
                TransformAxis = TransformType.RotateX
            };

            TranslationJoint r = new TranslationJoint
            {
                Length = l3,
                TransformAxis = TransformType.AxisZ
            };

            return new List<IJoint>
            {
                phi,
                theta,
                r
            };

        }


        public static List<IJoint> GetArticulated(double l1,double l2,double l3)
        {
            RotationJoint th1 = new RotationJoint
            {
                Length = l1,
                TransformAxis = TransformType.RotateZ
            };
            RotationJoint th2 = new RotationJoint
            {
                Length = l2,
                TransformAxis = TransformType.RotateX
            };
            RotationJoint th3 = new RotationJoint
            {
                Length = l3,
                TransformAxis = TransformType.RotateX
            };

            return new List<IJoint>
            {
                th1,
                th2,
                th3
            };
        }

        /// <summary>
        /// Calculate the Cartesian Inverse
        /// </summary>
        /// <param name="robot">The robot configuration. The order for this robot is not important</param>
        /// <param name="pos">The position of the robot</param>
        /// <returns>The joint transform</returns>

        public static Vector3F CartesianInverse(ref List<IJoint>robot, Vector3F pos)
        {
            if (robot.Count() != 3)
                throw new ArgumentException("The robot can only have 3 axis", "robot");
            if (((uint)(robot[0].TransformAxis | robot[1].TransformAxis | robot[2].TransformAxis)) != 0x07)
                throw new ArgumentException("The robot configuration error","robot");
            Vector3F result = new Vector3F();
            for (int i = 0;i<3;i++)
            {
                switch(robot[i].TransformAxis)
                {
                    case TransformType.AxisX:
                        result.X = pos.X - robot[i].Length;
                        robot[i].Transform.X = result.X;
                        break;
                    case TransformType.AxisY:
                        result.Y = pos.Y - robot[i].Length;
                        robot[i].Transform.Y = result.Y;
                        break;
                    case TransformType.AxisZ:
                        result.Z = pos.Z - robot[i].Length;
                        robot[i].Transform.Z = result.Z;
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Calculate the cylinical inverse
        /// </summary>
        /// <param name="robot">The robot configuration</param>
        /// <param name="pos">The robot endpoint position</param>
        /// <returns>The transform in the order of robot joint</returns>
        public static Vector3F CylindicalInverse(ref List<IJoint>robot, Vector3F pos)
        {
            if (robot.Count() != 3)
                throw new ArgumentException("The robot can only have 3 axis", "robot");
            if (robot[0].TransformAxis != TransformType.RotateZ)
                throw new ArgumentException("The first transformation axis must rotate on Z axis","robot");
            uint rst = (uint)(robot[1].TransformAxis | robot[2].TransformAxis);
            if (!(rst == 0x05 || rst == 0x06))
                throw new ArgumentException("Robot configuration error, the second joint and third can only translation");
            Vector3F vect = new Vector3F();
            if (rst == 0x05)
            {
                vect[0] = -Math.Atan2(pos.Y, pos.X);
            }
            else
            {
                vect[0] = Math.PI/2- Math.Atan2(pos.Y, pos.X);
            }
            robot[0].Transform.Z = vect[0];
            for(int i = 1;i<3;i++)
            {
                switch(robot[i].TransformAxis)
                {
                    case TransformType.AxisZ:
                        vect[i] = pos.Z - robot[0].Length - robot[i].Length;
                        robot[i].Transform.Z = vect[i];
                        break;
                    case TransformType.AxisX:
                        vect[i] = Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y) - robot[i].Length;
                        robot[i].Transform.X = vect[i];
                        break;
                    case TransformType.AxisY:
                        vect[i] = Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y) - robot[i].Length;
                        robot[i].Transform.Y = vect[i];
                        break;
                    
                }
            }

            return vect;

        }

        /// <summary>
        /// Calculate the spherical inverse
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector3F SphericalInverse(ref List<IJoint> robot, Vector3F pos)
        {
            if (robot.Count() != 3)
                throw new ArgumentException("The robot can only have 3 axis", "robot");
            if (!(robot[0].TransformAxis == TransformType.RotateZ && (robot[1].TransformAxis == TransformType.RotateX||robot[1].TransformAxis==TransformType.RotateY) && robot[2].TransformAxis == TransformType.AxisZ))
                throw new ArgumentException("The robot configuration cannot be accepted", "robot");
            Vector3F result = new Vector3F();
            result[2] = Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y + (pos.Z - robot[0].Length) * (pos.Z - robot[0].Length)) - robot[1].Length - robot[2].Length;
            robot[2].Transform.Z = result[2];
            result[1] = Math.Acos((pos.Z - robot[0].Length) / Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y + (pos.Z - robot[0].Length) * (pos.Z - robot[0].Length)));

            if (robot[1].TransformAxis == TransformType.RotateX)
            {
                result[0] = Math.Atan2(pos.X, pos.Y);
                robot[0].Transform.Z = result[0];
                robot[1].Transform.X = result[1];
            }
            else
            {
                result[0] = Math.PI / 2 - Math.Atan2(pos.Y, pos.X);
                robot[0].Transform.Z = result[0];
                robot[1].Transform.Y= result[1];
            }
            return result;
        }

        /// <summary>
        /// Calculate the Articulated Robot Inverse
        /// </summary>
        /// <param name="robot">The robot configuration</param>
        /// <param name="pos">point</param>
        /// <returns></returns>
        public static Vector3F ArticulatedInverse(ref List<IJoint> robot, Vector3F pos)
        {
            if (robot.Count() != 3)
                throw new ArgumentException("The robot can only have 3 axis", "robot");
            if (!(robot[0].TransformAxis == TransformType.RotateZ && (robot[1].TransformAxis == TransformType.RotateX )&& robot[2].TransformAxis == TransformType.RotateX))
                throw new ArgumentException("The robot configuration cannot be accepted", "robot");
            Vector3F result = new Vector3F();
            result[0] = Math.Atan2(pos.X, pos.Y);
            result[1] = Math.PI/2 - Math.Acos((robot[1].Length * robot[1].Length + pos.X * pos.X + pos.Y * pos.Y + (pos.Z - robot[0].Length) * (pos.Z - robot[0].Length) - robot[2].Length * robot[2].Length) / (2*robot[1].Length*Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y + (pos.Z - robot[0].Length) * (pos.Z - robot[0].Length))))-Math.Asin((pos.Z-robot[0].Length)/Math.Sqrt(pos.X*pos.X+pos.Y*pos.Y+(pos.Z-robot[0].Length)*(pos.Z-robot[0].Length)));
            result[2] = Math.PI - Math.Acos((robot[1].Length * robot[1].Length + robot[2].Length * robot[2].Length - pos.X * pos.X - pos.Y * pos.Y - (pos.Z - robot[0].Length) * (pos.Z - robot[0].Length)) / (2 * robot[1].Length * robot[2].Length));
            robot[0].Transform.Z = result[0];
            robot[1].Transform.X = result[1];
            robot[2].Transform.X = result[2];
            return result;
        }
    }
}
