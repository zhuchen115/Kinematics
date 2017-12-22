using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
    public class TranslationJoint : IJoint
    {
        /// <summary>
        /// The Translation Joint only have 1 DOF
        /// </summary>
        public int DOF => 1;

        /// <summary>
        /// The basic offset of the translation joint
        /// </summary>
        public double Length { get; set; }

        private Vector3F _length_vec
        {
            get
            {
                switch (_transformtype)
                {
                    case TransformType.AxisX:
                        return new Vector3F(Length, 0, 0);
                    case TransformType.AxisY:
                        return new Vector3F(0,Length,0);
                    case TransformType.AxisZ:
                        return new Vector3F(0, 0, Length);
                    default:
                        throw new ArgumentException("The Transform Type can only on translation", "TranslationJoint.Transform");
                }
            }
        }

        public Vector3F Transform {
            get {
                return _transform;
            }
            set {
                switch (_transformtype)
                {
                    case TransformType.AxisX:
                        _transform = new Vector3F(value.X, 0, 0);
                        break;
                    case TransformType.AxisY:
                        _transform = new Vector3F(0, value.Y, 0);
                        break;
                    case TransformType.AxisZ:
                        _transform = new Vector3F(0, 0, value.Z);
                        break;
                    default:
                        throw new ArgumentException("The Transform Type can only on translation", "TranslationJoint.Transform");
                }
            }
        }


        private Vector3F _transform = new Vector3F();
        private TransformType _transformtype = TransformType.None;

        /// <summary>
        /// The translation axis
        /// </summary>
        public TransformType TransformAxis {
            get => _transformtype;
            set {
                uint t = (uint)value;
                if (BitCal.BitCount(t & 0x07) != 1)
                    throw new ArgumentException("Only 1 degree of Freedom can be accepted", "TranslationJoint.TransformAxis");
                _transformtype = (TransformType)(t & 0x07);
            } }

        /// <summary>
        /// Get the endpoint position and orientation
        /// </summary>
        /// <param name="current">Previous head</param>
        /// <returns></returns>
        public Transform3D GetEndPointState(Transform3D current)
        {
            Vector3F rvec = (_transform+_length_vec).Rotate(current.Rotation);
            return new Transform3D(current.Position + rvec, current.Rotation);
        }
    }
}
