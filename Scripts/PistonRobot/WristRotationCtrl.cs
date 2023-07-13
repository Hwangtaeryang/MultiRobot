using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristRotationCtrl : MonoBehaviour
{
    public Transform wristObj;  //손목 회전 오브젝ㅌ트

    public Transform forearm;   //손목 회전 값 가져올 오브젝트

    float lastRotX, x;
    float joint0;

    float forearmFirst, forearmLast;    //손목 회전 할 수 있게 조건 값 부여변수


    void Start()
    {
        lastRotX = wristObj.rotation.z;
        forearmFirst = forearm.localPosition.x;    //손 위치값 초기값 받음
    }


    void Update()
    {
        //손 아래위로 움직이는 범위 -40~40로 범위지정. 그 이상 이하 로테이션값 안먹히게 함.
        //로봇 3D 모델링 손목 범위 -66~22로 지정하고 실제 손목 -40~40값을 받아와서 대입해서 값을 연동시킨다.
        //x = Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(forearm.transform).z, -7f, 80f);
        x = Mathf.Clamp(Mathf.DeltaAngle(0, forearm.localEulerAngles.z), -7f, 80f);
        joint0 = Mathf.Lerp(0f, 90f, Mathf.InverseLerp(-7f, 80f, x));

        //손 움직이는 위치값 받아옴
        forearmLast = forearm.localPosition.x;

        //Debug.Log(Mathf.Abs(forearmFirst - forearmLast));
        //손 지었다폈다 했을 때 손바닥 위치가 미세하게 변하는데 그때는 손목 움직이는걸 막는 조건문
        if (Mathf.Abs(forearmFirst - forearmLast) >= 0.0023f)
        {
            forearmFirst = forearmLast;
            WristRotationMove(joint0);
        }
        else
        {
            forearmFirst = forearmLast;
        }
    }

    public void WristRotationMove(float _move)
    {
        wristObj.Rotate(0f, 0f, (_move - lastRotX));
        lastRotX = _move;
    }
}
