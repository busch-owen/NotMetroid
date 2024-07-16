using UnityEngine;
using Cinemachine;
 
[SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
public class ApplyCorrections : CinemachineExtension //stolen from https://forum.unity.com/threads/cinemachine-virtual-camera-going-out-of-collision-bounds.1361239/
{
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            state.RawPosition = state.CorrectedPosition;
            state.PositionCorrection = Vector3.zero;
            state.RawOrientation = state.CorrectedOrientation;
            state.OrientationCorrection = Quaternion.identity;
        }
    }
}
