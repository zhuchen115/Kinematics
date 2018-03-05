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
            => new Vector3F(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
   
        
        /// <summary>
        /// Vector Minus
        /// </summary>
        /// <param name="vec1">First Vector</param>
        /// <param name="vec2">Second Vector</param>
        /// <returns>Vector</returns>
        public static Vector3F operator -(Vector3F vec1, Vector3F vec2)
            => new Vector3F(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);

        /// <summary>
        /// A vector times a number
        /// </summary>
        /// <param name="g">number</param>
        /// <param name="vec">the vector</param>
        /// <returns>new vector</returns>
        public static Vector3F operator *(double g, Vector3F vec)
            => new Vector3F(vec.X * g, vec.Y * g, vec.Z * g);

        /// <summary>
        /// Divide a vector by a number
        /// </summary>
        /// <param name="vec">the vector</param>
        /// <param name="g">the divisor, cannot be 0</param>
        /// <returns>Divided vector</returns>
        public static Vector3F operator /(Vector3F vec, double g)
        {
            if (g == 0)
                throw new DivideByZeroException("The divisior cannot be 0");
            return (1 / g) * vec;
        }

        /// <summary>
        /// Get the unit vector of a vector
        /// </summary>
        /// <param name="vec">The vector</param>
        /// <returns>A vector whose length is 1</returns>
        public static Vector3F UnitVector(Vector3F vec) => vec / vec.Length;


        /// <summary>
        /// Get the unit vector
        /// </summary>
        public Vector3F Unit
        {
            get  => UnitVector(this);
        }


        /// <summary>
        /// Vector dot product
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <returns>The dot product</returns>
        public static double operator *(Vector3F vec1, Vector3F vec2)
            => vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z;

        /// <summary>
        /// Out Product
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <returns>Out product of 2 vector</returns>
        public static Vector3F operator ^ (Vector3F vec1,Vector3F vec2)
            => new Vector3F(vec1.Y * vec2.Z - vec1.Z * vec2.Y, vec1.Z * vec2.X - vec1.X * vec2.Z, vec1.X * vec2.Y - vec1.Y * vec2.X);

        /// <summary>
        /// Calculate the angle between vectors
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <returns>The angle between two vectors</returns>
        public static double Angle(Vector3F vec1, Vector3F vec2)
            => Math.Acos((vec1 * vec2) / (vec1.Length * vec2.Length));
        

        /// <summary>
        /// Calculate the out product of two vectors
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <returns>result</returns>
        public static Vector3F OutProduct(Vector3F vec1, Vector3F vec2)
            => new Vector3F(vec1.Y * vec2.Z - vec1.Z * vec2.Y, vec1.Z * vec2.X - vec1.X * vec2.Z, vec1.X * vec2.Y - vec1.Y * vec2.X);


        /// <summary>
        /// Rotate a vector
        /// </summary>
        /// <param name="rotate">3D rotation </param>
        /// <returns>The rotated vector</returns>
        [Obsolete("Method is deprecated use the rotation matrix instead")]
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

   

    public class Vector3F2
    {
        /// <summary>
        /// The storage of the vectors
        /// </summary>
        public Vector3F vec1 = new Vector3F(), vec2 = new Vector3F();

        /// <summary>
        /// A quick access of the values 
        /// </summary>
        /// <param name="index">The number 0-5</param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                if (index < 3)
                    return vec1[index];
                else
                    return vec2[index - 3];
            }
            set
            {
                if (index < 3)
                    vec1[index] = value;
                else
                    vec2[index - 3] = value;
            }
        }

        /// <summary>
        /// Implement a vector convention to the array
        /// </summary>
        /// <param name="vect"></param>
        public static implicit operator Vector3F[] (Vector3F2 vect)
        {
            Vector3F[] vect_l = new Vector3F[2];
            vect_l[0] = vect.vec1;
            vect_l[1] = vect.vec2;
            return vect_l;
        }


        /// <summary>
        /// Use the explicit convention from array for the limited size
        /// </summary>
        /// <param name="vects"></param>
        public static explicit operator Vector3F2(Vector3F[] vects)
        {
            if (vects.Count() != 2)
                throw new ArgumentException("Only 2 vector array can be convert to Vector3F2");
            return new Vector3F2 { vec1 = vects[0], vec2 = vects[1] };
        }

        public override string ToString()
            => String.Format("({0}, {1}, {2}, {3}, {4}, {5})", vec1[0], vec1[1], vec1[2], vec2[0], vec2[1], vec2[2]);

    }

    public class Vector3F3
    {
        private Vector3F[] _vec = new Vector3F[3];
        
        public Vector3F this[int index]
        {
            get
            {
                return _vec[index];
            }
            set
            {
                _vec[index][0] = value[0];
                _vec[index][1] = value[1];
                _vec[index][2] = value[2];
            }
        }

        public Vector3F3(double[] values, bool order = true)
        {
            _vec[0] = new Vector3F();
            _vec[1] = new Vector3F();
            _vec[2] = new Vector3F();
            int i = 0;
            foreach (double val in values)
            {
                if (order)
                    _vec[i / 3][i % 3] = val;
                else
                    _vec[i % 3][i / 3] = val;
                i++;
                if (i == 9)
                    break;
            }
        }

        public Vector3F3() { }
    }
}
