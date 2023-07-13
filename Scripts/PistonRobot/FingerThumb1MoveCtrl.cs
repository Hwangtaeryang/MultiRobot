using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerThumb1MoveCtrl : MonoBehaviour
{
    public Transform finger1;   //움직일 대상
    public Transform finger2;

    public Transform finger_bone2;  //값 받아올 대상
    public Transform palm;

    float lastRotX, x;
    float joint0, joint1;
    float palmFirst, palmLast;
    float boneFirst, boneLast;


    void Start()
    {
        lastRotX = finger2.rotation.x;
        palmFirst = palm.localPosition.y;
        boneFirst = finger_bone2.localPosition.y;
    }


    void Update()
    {
        //x = Mathf.Clamp(Mathf.DeltaAngle(0, palm.localEulerAngles.x), -35f, 50);
        //x = UnityEditor.TransformUtils.GetInspectorRotation(finger_bone2.transform).y;
        //Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(finger_bone2.transform).x, 4f, 20f);

        x = Mathf.DeltaAngle(0, finger_bone2.localEulerAngles.y);

        joint0 = Mathf.Lerp(-40f, 0f, Mathf.InverseLerp(0f, 16f, x));


        palmLast = palm.localPosition.y;    //손바닥 아래위로 움직일 떄 값 받기
        boneLast = finger_bone2.localPosition.y;    //손가락 움직일때 위치 값

        //Debug.Log(Mathf.Abs(boneFirst - boneLast));
        palmFirst = palmLast;
        boneFirst = boneLast;
        FingerThumbMove(joint0);
    }

    public void FingerThumbMove(float _move)
    {
        //Debug.Log((_move - lastRotX));
        finger1.Rotate(0f, -(_move - lastRotX), 0f);
        finger2.Rotate(0f, -(_move - lastRotX), 0f);
        lastRotX = _move;
    }
}
