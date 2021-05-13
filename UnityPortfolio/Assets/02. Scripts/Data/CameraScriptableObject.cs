using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라 위치 데이터 값 담을 리소스
public class CameraScriptableObject : ScriptableObject
{
    public float Distance;
    public float TargetHeight;
    public float PitchValue;
    public float YawValue;
}