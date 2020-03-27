using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;
 
/// <summary>
/// Rotate the Follow Target to look along the camera forward axis
/// </summary>
[SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
public class RotateTargetToMatch : CinemachineExtension
{
    public bool m_YRotationOnly = true;
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Aim)
        {
            var follow = VirtualCamera.Follow;
            if (follow != null)
            {
                Vector3 fwd = state.RawOrientation * Vector3.forward;
                if (m_YRotationOnly)
                    fwd = fwd.ProjectOntoPlane(state.ReferenceUp);
                follow.rotation = Quaternion.LookRotation(fwd, state.ReferenceUp);
            }
        }
    }
}
