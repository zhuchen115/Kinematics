using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
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
        public Vector3F Rotation {
            get
            {
                return RMatrix2Euler(RotationMat);
            }

            private set {
                RotationMat = Euler2RMatrix(value);
            }
        }

        /// <summary>
        /// The Rotation Matrix 
        /// </summary>
        public Matrix3F RotationMat { get; set; }

       
        /// <summary>
        /// Initial empty state
        /// </summary>
        public Transform3D()
        {
            Position = new Vector3F();
            RotationMat = Matrix3F.GetUnitMatrix();
        }

        /// <summary>
        /// Initial with known postion and rotation vectors
        /// </summary>
        /// <param name="pos">position of the ridigbody</param>
        /// <param name="rotate">rotation of the ridigbody (Euler Angle)</param>
        public Transform3D(Vector3F pos, Vector3F rotate)
        {
            Position = pos;
            Rotation = rotate;
        }

        /// <summary>
        /// Initialize with a rotation matrix
        /// </summary>
        /// <param name="pos">The point position</param>
        /// <param name="rotate">The point rotation</param>
        public Transform3D(Vector3F pos,Matrix3F rotate)
        {
            Position = pos;
            RotationMat = rotate;
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

        /// <summary>
        /// Copy the object 
        /// </summary>
        /// <returns>A new transform 3d object</returns>
        public object Clone()
        {
            return new Transform3D((Vector3F)Position.Clone(), (Matrix3F)RotationMat.Clone());
        }

        public override string ToString()
        {
            return String.Format("Pos {0},Rot {1}", Position, Rotation);
        }

        /// <summary>
        /// Convert the rotation matrix to the euler angle
        /// </summary>
        /// <param name="rmat">The roatation matrix</param>
        /// <returns>The Euler Angle vector</returns>
        public static Vector3F RMatrix2Euler(Matrix3F rmat)
        {
            return new Vector3F(
                Math.Atan2(rmat[2, 1], rmat[2, 2]),
                Math.Atan2(-rmat[2, 0], Math.Sqrt(rmat[2, 1] * rmat[2, 1] + rmat[2, 2] * rmat[2, 2])),
                Math.Atan2(rmat[1, 0], rmat[0, 0]));
        }

        /// <summary>
        /// Convert the Euler Angle to the Rotation Matrix
        /// </summary>
        /// <param name="euler">Euler Angle Vector</param>
        /// <returns>Rotation Matrix</returns>
        public static Matrix3F Euler2RMatrix(Vector3F euler)
        {
            double sx = Math.Sin(euler.X), cx = Math.Cos(euler.X),
                sy = Math.Sin(euler.Y), cy = Math.Cos(euler.Y),
                sz = Math.Sin(euler.Z), cz = Math.Cos(euler.Z);
            return new Matrix3F(
                cy * cz, cz * sx * sy - cx * sz, sx * sz + cx * cz * sy,
                cy * sz, cx * cz + sx * sy * sz, cx * sy * sz - sx * cz,
                -sy,     cy * sx,                cx * cy
                );
        }
    }
}
