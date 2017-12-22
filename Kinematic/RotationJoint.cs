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

            //The vector on the local coordinate system
            Vector3F local_vec = new Vector3F();
            
            //Assume initial state on the z axis
            switch(_transformtype)
            {
                //The rotation on x axis, clockwise on y-z panel
                case TransformType.RotateX:
                    local_vec = new Vector3F(0,Length*Math.Sin(_transform.X),Length*Math.Cos(_transform.X));
                    tf3d.Rotation.X += _transform.X;
                    break;
                //The rotation on y axis, clockwise on z-x panel
                case TransformType.RotateY:
                    local_vec = new Vector3F(-Length * Math.Sin(_transform.Y), 0, Length * Math.Cos(_transform.Y));
                    tf3d.Rotation.Y += _transform.Y;
                    break;
                // The rotation on Z axis, clockwise on x-y panel
                case TransformType.RotateZ:
                    local_vec = new Vector3F(0, 0, Length);
                    tf3d.Rotation.Z += _transform.Z;
                    break;
            }
            //Rotate the vector to the previous coordinate system
            tf3d.Position = current.Position + local_vec.Rotate(current.Rotation);
            tf3d.Rotation += current.Rotation;

            return tf3d;
        }


    }
}
