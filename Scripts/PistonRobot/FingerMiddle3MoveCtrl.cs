using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMiddle3MoveCtrl : MonoBehaviour
{
    public Transform finger1;   //움직일 대상
    public Transform finger2;
    public Transform finger3;

    public Transform finger_bone; //값 받아올 대상
    public Transform palm;

    float lastRotX, x;
    float joint0, joint1, joint2;
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
        //x = Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(finger_bone.transform).x, 1f, 85f);
        x = Mathf.Clamp(Mathf.DeltaAngle(0, finger_bone.localEulerAngles.x), 1f, 85f); 
        joint0 = Mathf.Lerp(0f, 80f, Mathf.InverseLerp(1f, 85f, x));

        palmLast = palm.localPosition.y;

        boneLast = finger_bone.localPosition.y;

        //Debug.Log(Mathf.Abs(palmFirst - palmLast));
        if (Mathf.Abs(palmFirst - palmLast) <= 0.005f && Mathf.Abs(boneFirst - boneLast) <= 0.08f)
        {
            palmFirst = palmLast;
            boneFirst = boneLast;
            FingerMiddleMove(joint0);
        }
        else
        {
            palmFirst = palmLast;
            boneFirst = boneLast;
        }
    }

    public void FingerMiddleMove(float _move)
    {
        finger1.Rotate(0f, -(_move - lastRotX), 0f);
        finger2.Rotate(0f, -(_move - lastRotX), 0f);
        finger3.Rotate(0f, -(_move - lastRotX), 0f);
        lastRotX = _move;
    }
}
