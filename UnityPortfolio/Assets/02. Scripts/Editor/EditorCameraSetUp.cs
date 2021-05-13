using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using KSW;

public static class EditorCameraSetUp
{
    //씬 뷰에 있는 카메라를 현재 ThirdPersonCamera에 세팅된 값으로 배치 합니다.
    [MenuItem("CONTEXT/ThirdPersonCamera/SetUpGameView")]
    private static void SetUpGameView(MenuCommand menuCommand)
    {
        ThirdPersonCamera target = null;
        if (menuCommand.context is ThirdPersonCamera == false)
            return;
        target = menuCommand.context as ThirdPersonCamera;
        target.UpdateAutoCamera();
    }

    //현재 카메라 조정 값을 ScriptableObject로 저장합니다. 
    //해당 기능으로 만들어진 리소스는 인 게임 카메라 세팅에서 사용합니다
    [MenuItem("CONTEXT/ThirdPersonCamera/CreateData")]
    private static void CreateData(MenuCommand menuCommand)
    {
        ThirdPersonCamera target = null;
        if (menuCommand.context is ThirdPersonCamera == false)
            return;
        target = menuCommand.context as ThirdPersonCamera;

        CameraScriptableObject dataObject = ScriptableObject.CreateInstance<CameraScriptableObject>();
        
        dataObject.Distance = target.distance;
        dataObject.TargetHeight = target.targetHeight;
        dataObject.YawValue = target.yaw;
        dataObject.PitchValue = target.pitch;
        AssetDatabase.CreateAsset(dataObject, "Assets/Resources/Camera/Data.asset");
    }
}
