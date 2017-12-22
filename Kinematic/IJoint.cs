using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{

    [Flags]
    public enum TransformType :uint
    {
        None = 0x00,
        AxisX = 0x01,
        AxisY = 0x02,
        AxisZ = 0x04,
        RotateX = 0x10,
        RotateY = 0x20,
        RotateZ = 0x40
    }
    /// <summary>
    /// The Interface for the joint
    /// </summary>
    public interface IJoint
    {
        /// <summary>
        /// The degree of freedom
        /// </summary>
        /// <remarks>
        /// The DOF should be a small as possibe to simplify the calculation
        /// </remarks>
        int DOF { get; }

        /// <summary>
        /// The length no Next Joint, Initial state
        /// </summary>
        double Length { get; set; }

        /// <summary>
        /// Get the state of the endpint of this link
        /// </summary>
        /// <param name="current">The base state of this joint</param>
        /// <returns></returns>
        Transform3D GetEndPointState(Transform3D current);

        /// <summary>
        /// The transformation of the joint 
        /// </summary>
        Vector3F Transform { get; set; }

        /// <summary>
        /// The axis to be moved.
        /// </summary>
        TransformType TransformAxis { get; set; }
    }

}
