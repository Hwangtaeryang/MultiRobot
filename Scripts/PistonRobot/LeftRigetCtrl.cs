using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRigetCtrl : MonoBehaviour
{
    public Transform armBodyObj;    //움직일 오브젝트
    public Transform leftRightStick;    //같이 움직이 스틱
    public Transform palmX; //값 받아올 오브젝트

    float firstPosX, lastPosX, xBody, _xBody, _xLeftRight;
    float palmFirst, palmLast;  //손폈다줬다 움직임 조건문 줄때 변수

    void Start()
    {
        lastPosX = armBodyObj.localPosition.x;
        palmFirst = palmX.localPosition.x;  //처음 손바닥 x값 저장
    }

    
    void Update()
    {
        //팔 움직였을때 최소, 최대값 지정해서 그 이상이하 못 나오게 한다.
        xBody = Mathf.Clamp(palmX.localPosition.x, -0.4f, 0.4f);

        //움직이는 팔의 범위와 움직여야하는 오브젝트의 범위를 적용 시킴.
        _xBody = Mathf.Lerp(852f, 1641f, Mathf.InverseLerp(-0.4f, 0.4f, xBody));

        //움직이는 팔의 범위와 움직여야하는 봉 오브젝트의 범위를 적용시킴
        _xLeftRight = Mathf.Lerp(-1715f, -928f, Mathf.InverseLerp(-0.4f, 0.4f, xBody));
        //Debug.Log(xBody);

        palmLast = palmX.localPosition.x;   //후 손바닥 x값 저장

        //Debug.Log(Mathf.Abs(palmFirst - palmLast));
        //손 움직였을때(손폈다졌다) 좌우로 움직이지 못하게 조건문
        if (Mathf.Abs(palmFirst - palmLast) >= 0.005f)
        {
            palmFirst = palmLast;   //처음 변수에 움직인 후 변수 저장
            ArmBodyMove(_xBody);
            LeftRightMove(_xLeftRight);
        }
        else
        {
            palmFirst = palmLast;
        }
    }

    //손 본체 움직이는 함수
    public void ArmBodyMove(float _moveX)
    {
        float gapValue;
        gapValue = lastPosX - _moveX;
        gapValue = Mathf.Abs(gapValue); //절대값

        if(gapValue >= 5f)
        {
            armBodyObj.localPosition = new Vector3(_moveX, armBodyObj.localPosition.y, armBodyObj.localPosition.z);
            lastPosX = _moveX;
        }
    }

    //좌우로 움직이는 스틱 움직이는 함수
    public void LeftRightMove(float _moveX)
    {
        float gapValue;
        gapValue = lastPosX - _moveX;
        gapValue = Mathf.Abs(gapValue);

        //Debug.Log(gapValue);

        if (gapValue >= 5f)
        {
            leftRightStick.localPosition = new Vector3(_moveX, leftRightStick.localPosition.y, leftRightStick.localPosition.z);
            lastPosX = _moveX;
        }
    }
}
