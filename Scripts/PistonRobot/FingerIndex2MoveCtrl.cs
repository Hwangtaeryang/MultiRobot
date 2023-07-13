using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerIndex2MoveCtrl : MonoBehaviour
{
    public Transform finger1;   //움직일 대상
    public Transform finger2;
    public Transform finger3;
    public Transform finger_bone; //값 받아올 대상
    public Transform palm;

    float lastRotX, x;
    float joint0, joint1, joint2;
    float palmPosFirst, palmPosLast;    //손바닥 위아래 움직일때 손가락 움직이는거 막기위한 변수(손바닥 포지션 처음값, 나중값)
    float palmRotFirst, palmRotLast;    //손목 움직였을때 손가락 움직이는 막기 위한 변수(손바닥 로테이션 처음값, 나중값)
    float boneFirst, boneLast;  //손바닥 위아래 움직일때 손가락 움직이는거 막기위한 변수(손가락 위치 처음값, 나중값)
    

    void Start()
    {
        lastRotX = finger2.rotation.x;
        //palmFirstX = UnityEditor.TransformUtils.GetInspectorRotation(palm.transform).x;
        palmPosFirst = palm.localPosition.y;
        boneFirst = finger_bone.localPosition.y;
        //palmRotFirst = UnityEditor.TransformUtils.GetInspectorRotation(palm.transform).x;
        palmRotFirst = Mathf.DeltaAngle(0, palm.localEulerAngles.x);
    }

    
    void Update()
    {
        //손가락 움직이는 범위 값 지정
        //1~85범위는 립모션에서 finger_bone2 오브젝트의 로테이션x값을 보고 지정해줌
        //x = Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(finger_bone.transform).x, -18f, 30f);
        x = Mathf.Clamp(Mathf.DeltaAngle(0, finger_bone.localEulerAngles.x), -18f, 30f);
        joint0 = Mathf.Lerp(0f, 80f, Mathf.InverseLerp(-18f, 30f, x));

        //손바닥 아래위로 움직일 떄 값 받기
        palmPosLast = palm.localPosition.y;

        //손가락움직일때 위치값
        boneLast = finger_bone.localPosition.y;

        //손바닥(손목) 아래위로 움직일때 값 받기
        //palmRotLast = UnityEditor.TransformUtils.GetInspectorRotation(palm.transform).x;
        palmRotLast= Mathf.DeltaAngle(0, palm.localEulerAngles.x);

        //Mathf.Abs(palmFirstX - palmLastX) <= 20f :: 손바닥 위아래로 움직일때 손가락 로테이션값이 바꿔서 그걸 막아주기 위한 조건식
        //손바닥 트렌스폼 y값이 작게 변할땐 움직이고, 크게 변할땐 안움직이게 함(주먹폈다졌다)
        //Mathf.Abs(boneFirst - boneLast) <= 0.007f :: 손바닥 위아래로 움직일때 손가락 로테이션값이 바껴서 그걸 막아주기 위한 조건식
        //손가락 트렌스폼 y값이 작게 변할땐 움직이고, 크게 변할때는 안움직이게 함(주먹폈다졌다)
        //Mathf.Abs(palmRotFirst - palmRotLast) <= 4.5f :: 손목(손바닥)을 아래위로 움직일때 손가락 로테이션값이 바꿔서 그걸 막기위한 조건식
        //손목(손바닥) 로테이션x값이 작게 변할때 움직이고, 크게 변할땐 안움직이게 함(주먹 지었다폈다)
        //Debug.Log(Mathf.Abs(palmRotFirst - palmRotLast));
        if (Mathf.Abs(palmPosFirst - palmPosLast) <= 0.007f && Mathf.Abs(boneFirst - boneLast) <= 0.03f)// && Mathf.Abs(palmRotFirst - palmRotLast) <= 0.9f)
        {
            palmPosFirst = palmPosLast;
            boneFirst = boneLast;
            palmRotFirst = palmRotLast;
            HandMoveControll(joint0);
        }
        else
        {
            palmPosFirst = palmPosLast;
            boneFirst = boneLast;
            palmRotFirst = palmRotLast;
        }
        
    }


    public void HandMoveControll(float _moveX)
    {
        finger1.Rotate(0f, -(_moveX - lastRotX), 0f);
        finger2.Rotate(0f, -(_moveX - lastRotX), 0f);
        finger3.Rotate(0f, -(_moveX - lastRotX), 0f);
        lastRotX = _moveX;
    }
}
