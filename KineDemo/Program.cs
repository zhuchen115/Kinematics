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
            List<IJoint> joints = Robots3DOF.GetArticulated(1, 0.87, 0.54);
            Transform3D base_axis = new Transform3D(new Vector3F(0, 0, 0), new Vector3F(0, 0, 0));

            Robots3DOF.ArticulatedInverse(ref joints, new Vector3F(2.8, 4.2, 3.5));
            Transform3D tf3d = base_axis;
            foreach (IJoint jo in joints)
            {
                tf3d = jo.GetEndPointState(tf3d);
            }

            Console.WriteLine("Cartesian");
            Console.WriteLine("The endpoint position is {0}, rotation is {1}", tf3d.Position, tf3d.Rotation);
            Console.ReadLine();

        }
    }
}
