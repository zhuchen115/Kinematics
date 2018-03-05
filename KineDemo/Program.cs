using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kinematic;

namespace KineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IJoint> joints = Robot6DOF.Get6DOFRobot(2, 2, 2, 2, 2, 2);
            Transform3D base_axis = new Transform3D(new Vector3F(0, 0, 0), new Vector3F(0, 0, 0));

            Transform3D tf3d = base_axis;
            tf3d.RotationMat = Matrix3F.GetUnitMatrix();

            joints[0].Transform = new Vector3F(0, 0, Math.PI * 4 / 6);
            joints[1].Transform = new Vector3F(0, Math.PI * 5 / 6, 0);
            joints[2].Transform = new Vector3F(0, Math.PI  / 6, 0);
            joints[3].Transform = new Vector3F(0, 0, Math.PI / 6);
            joints[4].Transform = new Vector3F(0, Math.PI / 3, 0);
            joints[5].Transform = new Vector3F(0, 0, Math.PI / 4);

            foreach (IJoint jo in joints)
            {
                tf3d = jo.GetEndPointState(tf3d);
            }
           
            Console.WriteLine("6DOF");

            Vector3F2 transform = Robot6DOF.Inverse6DOF(ref joints, tf3d);
            Console.WriteLine("The endpoint position is {0}, euler rotation is {1}", tf3d.Position - base_axis.Position, tf3d.Rotation);
            Console.WriteLine("Robot Joint Transform: {0}", transform.ToString());
            Console.ReadLine();

        }
    }
}
