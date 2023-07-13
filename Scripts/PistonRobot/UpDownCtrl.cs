using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCtrl : MonoBehaviour
{
    public Transform armBodyObj;    //움직일 오브젝트
    public Transform updownStick;   //같이 움직일 스틱
    public Transform forearm; //값 받아올 오브젝트 팔
    public Transform finger_bone;

    float lastPosY, yBody, _yBody, _yUpDown;
    float forearmFirst, forearmLast;  //손폈다줬다 움직임 조건문 줄때 변수
    float boneFirst, boneLast;

    void Start()
    {
        lastPosY = armBodyObj.localPosition.y;
        forearmFirst = forearm.localPosition.y;  //처음 손바닥 y값 저장
        boneFirst = finger_bone.localPosition.y;
    }

    
    void Update()
    {
        //팔 움직였을때 최소, 최대값 지정해서 그 이상이하 못 나오게 한다.
        yBody = Mathf.Clamp(forearm.localPosition.y,0.1f, 0.65f);

        //움직이는 팔의 범위와 움직여야하는 오브젝트의 범위를 적용?시킴.
        _yBody = Mathf.Lerp(33, 825, Mathf.InverseLerp(0.1f, 0.65f, yBody));

        //움직이는 팔의 범위와 움직여야하는 봉 오브젝트의 범위를 적용시킴
        _yUpDown = Mathf.Lerp(-1177, -383, Mathf.InverseLerp(0.1f, .65f, yBody));

        forearmLast = forearm.localPosition.y;   //후 손바닥 y값 저장
        boneLast = finger_bone.localPosition.y;

        //Debug.Log(Mathf.Abs(boneFirst - boneLast));
        //손 움직였을때(손폈다졌다) 아래위로 움직이지 못하게 조건문
        //Mathf.Abs(forearmFirst - forearmLast) >= 0.005f :: 팔 위아래 움직이는 값
        //Mathf.Abs(boneFirst - boneLast) >= 0.008f :: 손가락 트렌스폼 y 주먹폈다졌다 값
        if (Mathf.Abs(forearmFirst - forearmLast) >= 0.005f && Mathf.Abs(boneFirst - boneLast) >= 0.008f)
        {
            forearmFirst = forearmLast;   //처음 변수에 움직인 후 변수 저장
            boneFirst = boneLast;

            ArmBodyMove(_yBody);    //손본체
            UpDownStickMove(_yUpDown);  //아래위 스틱
        }
        else
        {
            boneFirst = boneLast;
            forearmFirst = forearmLast;
        }
    }

    //손 본체 움직이는 함수
    public void ArmBodyMove(float _moveY)
    {
        float gapValue;
        gapValue = lastPosY - _moveY;
        gapValue = Mathf.Abs(gapValue);

        if(gapValue >= 5f)
        {
            armBodyObj.localPosition = new Vector3(armBodyObj.localPosition.x, _moveY, armBodyObj.localPosition.z);
            lastPosY = _moveY;
        }
    }

    //위아래로 움직이는 스틱 움직이는 함수
    public void UpDownStickMove(float _moveY)
    {
        float gapValue;
        gapValue = lastPosY - _moveY;
        gapValue = Mathf.Abs(gapValue);

        if (gapValue >= 5f)
        {
            updownStick.localPosition = new Vector3(updownStick.localPosition.x, _moveY, updownStick.localPosition.z);
            lastPosY = _moveY;
        }
    }
}
