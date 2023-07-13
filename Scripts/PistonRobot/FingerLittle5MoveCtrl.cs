using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerLittle5MoveCtrl : MonoBehaviour
{
    public Transform finger1;   //움직일 오브젝트
    public Transform finger2;
    public Transform finger3;

    public Transform finger_bone;   //값 받을 오브젝(손가락)
    public Transform palm;  //값 받을 오브젝(손바닥)

    float lastRotX, x;
    float joint0, joint1, joint2;   //변경값 받을 변수
    float palmFirst, palmLast;
    float boneFirst, boneLast;


    void Start()
    {
        lastRotX = finger2.rotation.x;
        palmFirst = palm.localPosition.y;
        boneFirst = finger_bone.localPosition.y;
    }

    
    void Update()
    {
        //손가락 움직이는 범위 값 지정
        //1~85범위는 립모션에서 finger_bone 오브젝트의 로테이션x값을 보고 지정해줌
        //손가락 움직이는 각 0~80값을 1~85사이에서 받는 값을 대입해서 사이값으로 변경시켜준다.
        //x = Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(finger_bone.transform).x, 1f, 85f);
        x = Mathf.Clamp(Mathf.DeltaAngle(0, finger_bone.localEulerAngles.x), 1f, 85f);
        joint0 = Mathf.Lerp(0f, 80f, Mathf.InverseLerp(1f, 85f, x));

        palmLast = palm.localPosition.y;

        boneLast = finger_bone.localPosition.y;

        //Debug.Log(Mathf.Abs(palmFirst - palmLast));
        //
        if (Mathf.Abs(palmFirst - palmLast) <= 0.2f && Mathf.Abs(boneFirst - boneLast) <= 0.09f)
        {
            palmFirst = palmLast;
            boneFirst = boneLast;
            FingerLittleMove(joint0);
        }
        else
        {
            palmFirst = palmLast;
            boneFirst = boneLast;
        }
    }

    public void FingerLittleMove(float _move)
    {
        finger1.Rotate(0f, -(_move - lastRotX), 0f);
        finger2.Rotate(0f, -(_move - lastRotX), 0f);
        finger3.Rotate(0f, -(_move - lastRotX), 0f);
        lastRotX = _move;
    }
}
