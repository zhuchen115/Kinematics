using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
    /// <summary>
    /// A Vector using double
    /// </summary>
    [Serializable]
    public class Vector3F : ICloneable
    {
        /// <summary>
        /// Initialize with 0
        /// </summary>
        public Vector3F()
        {
            X = 0; Y = 0; Z = 0;
        }

        /// <summary>
        /// Initialize with values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3F(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        /// <summary>
        /// Get the Length of the vector
        /// </summary>
        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary>
        /// Vector Addition
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>Add Vector</returns>
        public static Vector3F operator +(Vector3F vec1, Vector3F vec2)
        {
            return new Vector3F(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }
        
        /// <summary>
        /// Vector Minus
        /// </summary>
        /// <param name="vec1">First Vector</param>
        /// <param name="vec2">Second Vector</param>
        /// <returns>Vector</returns>
        public static Vector3F operator -(Vector3F vec1, Vector3F vec2)
        {
            return new Vector3F(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        /// <summary>
        /// A vector times a number
        /// </summary>
        /// <param name="g">number</param>
        /// <param name="vec">the vector</param>
        /// <returns>new vector</returns>
        public static Vector3F operator *(double g, Vector3F vec)
        {
            return new Vector3F(vec.X * g, vec.Y * g, vec.Z * g);
        }


        /// <summary>
        /// Vector dot product
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <returns>The dot product</returns>
        public static double operator *(Vector3F vec1, Vector3F vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z;
        }


        /// <summary>
        /// Calculate the angle between vectors
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <returns>The angle between two vectors</returns>
        public static double Angle(Vector3F vec1, Vector3F vec2)
        {
            return Math.Acos((vec1 * vec2) / (vec1.Length * vec2.Length));
        }

        /// <summary>
        /// Calculate the out product of two vectors
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <returns>result</returns>
        public static Vector3F OutProduct(Vector3F vec1, Vector3F vec2)
        {
            return new Vector3F(vec1.Y * vec2.Z - vec1.Z * vec2.Y, vec1.Z * vec2.X - vec1.X * vec2.Z, vec1.X * vec2.Y - vec1.Y * vec2.X);
        }

        /// <summary>
        /// Rotate a vector
        /// </summary>
        /// <param name="rotate">3D rotation </param>
        /// <returns>The rotated vector</returns>
        public Vector3F Rotate(Vector3F rotate)
        {
            Vector3F result = (Vector3F)Clone();
            if (rotate.X != 0)
                result = result._rotate_x(rotate.X);
            if (rotate.Y != 0)
                result = result._rotate_y(rotate.Y);
            if (rotate.Z != 0)
                result = result._rotate_z(rotate.Z);
            return result;
        }

        /// <summary>
        /// Rotate around x Axis
        /// </summary>
        /// <param name="theta">the angle to be rotated</param>
        /// <returns>The rotated vector</returns>
        private Vector3F _rotate_x(double theta)
        {
            return new Vector3F(X, Y * Math.Cos(theta) + Z * Math.Sin(theta), -Y * Math.Sin(theta) + Z * Math.Cos(theta));
        }

        /// <summary>
        /// Rotate around y Axis
        /// </summary>
        /// <param name="theta">the angle to be rotated</param>
        /// <returns>The rotated vector</returns>
        private Vector3F _rotate_y(double theta)
        {
            return new Vector3F(X * Math.Cos(theta) - Z * Math.Sin(theta), Y, X * Math.Sin(theta) + Z * Math.Cos(theta));
        }

        /// <summary>
        /// Rotate around z Axis
        /// </summary>
        /// <param name="theta">the angle to be rotated</param>
        /// <returns>The rotated vector</returns>
        private Vector3F _rotate_z(double theta)
        {
            return new Vector3F(X * Math.Cos(theta) + Y * Math.Sin(theta), -X * Math.Sin(theta) + Y * Math.Cos(theta), Z);
        }

        /// <summary>
        /// Copy the object
        /// </summary>
        /// <returns>new vectors</returns>
        public object Clone()
        {
            return new Vector3F(X, Y, Z);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", X, Y, Z);
        }

        /// <summary>
        /// Get the indexers
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                int i = index % 3;
                switch(i)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        throw new NullReferenceException("Index not found");
                }
            }
            set
            {
                int i = index % 3;
                switch (i)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new NullReferenceException("Index not found");
                }
            }
        }

        public bool IsValid
        {
            get { return !(Double.IsNaN(X) || Double.IsNaN(Y) || Double.IsNaN(Z)); }
        }

    }

    /// <summary>
    /// The Type of Axis used
    /// </summary>
    public enum Axis3DCoordinate
    {
        /// <summary>
        /// Cartesian coordinate use [X Y Z]
        /// </summary>
        Cartesian,

        /// <summary>
        /// Cylindrical coordinate [l theta h]
        /// </summary>
        Cylindrical,

        /// <summary>
        /// Spherical coordinate [r rho theta]
        /// </summary>
        Spherical
    }

    /// <summary>
    /// Indicate the state of a rigidbody
    /// </summary>
    [Serializable]
    public class Transform3D : ICloneable
    {

        /// <summary>
        /// Position of point in cartesian
        /// </summary>
        public Vector3F Position { get; set; }

        /// <summary>
        /// Rotation in Eula rotation
        /// </summary>
        public Vector3F Rotation { get; set; }

        /// <summary>
        /// Initial empty state
        /// </summary>
        public Transform3D()
        {
            Position = new Vector3F();
            Rotation = new Vector3F();
        }

        /// <summary>
        /// Initial with known postion and rotation vectors
        /// </summary>
        /// <param name="pos">position of the ridigbody</param>
        /// <param name="rotate">rotation of the ridigbody</param>
        public Transform3D(Vector3F pos, Vector3F rotate)
        {
            Position = pos;
            Rotation = rotate;
        }

        /// <summary>
        /// Initial with known postion and rotation vector
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value</param>
        /// <param name="z">z value</param>
        /// <param name="rx">x axis rotation</param>
        /// <param name="ry">y axis rotation</param>
        /// <param name="rz">z axis rotation</param>
        public Transform3D(double x, double y, double z, double rx, double ry, double rz)
        {
            Position = new Vector3F(x, y, z);
            Rotation = new Vector3F(rx, ry, rz);
        }

        public object Clone()
        {
            return new Transform3D((Vector3F)Position.Clone(), (Vector3F)Rotation.Clone());
        }

        public override string ToString()
        {
            return String.Format("Pos {0},Rot {1}", Position, Rotation);
        }
    }
}
