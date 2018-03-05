using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
    /// <summary>
    /// Indicate a rotation joint
    /// </summary>
    public class RotationJoint : IJoint
    {
        /// <summary>
        /// The rotation joint should have only one degree of freedom
        /// </summary>
        public int DOF => 1;

        /// <summary>
        /// The link length
        /// </summary>
        /// 
        public double Length { get; set; }

        /// <summary>
        /// The joint transform
        /// </summary>
        /// <remarks>
        /// The rotation can only be on one axis only one value in the vector can be set 
        /// </remarks>
        public Vector3F Transform {
            get { return _transform; }
            set {
                switch(_transformtype)
                {
                    case TransformType.RotateX:
                        _transform = new Vector3F(value.X, 0, 0);
                        break;
                    case TransformType.RotateY:
                        _transform = new Vector3F(0, value.Y, 0);
                        break;
                    case TransformType.RotateZ:
                        _transform = new Vector3F(0, 0, value.Z);
                        break;
                    default:
                        throw new ArgumentException("The Transform Type can only on rotation","RotationJoint.Transform");
                }
            } }

        /// <summary>
        /// The rotation 
        /// </summary>
        /// <remarks>
        /// Only 1 rotation DOF can be accepted 
        /// </remarks>
        public TransformType TransformAxis {
            get => _transformtype;
            set {
                uint t = (uint)value;
                if (BitCal.BitCount(t & 0x70) != 1)
                    throw new ArgumentException("Only 1 degree of Freedom can be accepted","RotationJoint.TransformAxis");
                _transformtype = (TransformType)(t & 0x70);
            } }


        private Vector3F _transform = new Vector3F();
        private TransformType _transformtype = TransformType.None;

        /// <summary>
        /// Get the endpoint position and orientation.
        /// </summary>
        /// <param name="current">The base coordinate system</param>
        /// <returns>The endpoint position and rotation</returns>
        public Transform3D GetEndPointState(Transform3D current)
        {
            Transform3D tf3d = new Transform3D();

            
            
            //Assume initial state on the z axis
            switch(_transformtype)
            {
                //The rotation on x axis, anti-clockwise on y-z panel
                case TransformType.RotateX:
                    
                    tf3d.RotationMat = new Matrix3F(
                        1, 0, 0, 
                        0, Math.Cos(_transform.X), -Math.Sin(_transform.X), 
                        0, Math.Sin(_transform.X), Math.Cos(_transform.X));
                    break;
                //The rotation on y axis, anti-clockwise on z-x panel
                case TransformType.RotateY:
                    tf3d.RotationMat = new Matrix3F(
                        Math.Cos(_transform.Y),0,Math.Sin(_transform.Y),
                        0,1,0,
                        -Math.Sin(_transform.Y),0,Math.Cos(_transform.Y));
                    break;
                // The rotation on Z axis, anti-clockwise on x-y panel
                case TransformType.RotateZ:
                    tf3d.RotationMat = new Matrix3F(
                        Math.Cos(_transform.Z), -Math.Sin(_transform.Z), 0,
                        Math.Sin(_transform.Z), Math.Cos(_transform.Z), 0,
                        0, 0, 1);
                    break;
            }

            tf3d.RotationMat =   current.RotationMat* tf3d.RotationMat;
            Vector3F rotvec = tf3d.RotationMat * new Vector3F(0,0,Length);

            tf3d.Position = current.Position + rotvec;

            return tf3d;
        }


    }
}
