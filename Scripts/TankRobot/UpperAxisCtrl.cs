using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpperAxisCtrl : MonoBehaviour
{
    public Transform upperAxisPos;  //움직일 모델 부분
    public Transform palmZ;  //값 받을 모델

    public Slider upperUpDownSlider;
    public TextMeshProUGUI upperUpDownValueText;

    float rotZ, lastRotZ, z;

    public static float _x;


    void Start()
    {
        //초기값 세팅
        //upperAxis오브젝트 로테이션 x값 초기화
        //rotZ = upperAxisPos.transform.position.z;
        //lastRotZ = rotZ;
        lastRotZ = upperAxisPos.transform.position.x;
    }

    
    void Update()
    {
        //palm값을 최소 -50, 최대 50 범위 지정해서 그 사이에서 값이 있게 한다.
        //움직이는 범위가 소수점에서 왔다갔다해서 100곱해서 크기를 키운다.
        z = Mathf.Clamp(palmZ.transform.localPosition.z*100 , -50f, 50f);

        //소수점 두번째 자리까지 짜른다.
        z = Mathf.Round(z * 100) * 0.01f;

        //-10~25사이 들어오는값을 -50~50범위로 바꾼다.
        _x = Mathf.Lerp(-50f, 50f, Mathf.InverseLerp(-10f, 25f, z));

        //슬라이더와 텍스터에 보낸다.
        upperUpDownSlider.value = _x;
        upperUpDownValueText.text = _x.ToString(); 


        if (DegreeRobotClass.GetSliderMove())
        {
            //슬라이더바 업데이트
            upperUpDownSlider.value = DegreeRobotClass.GetAxisAngle3();
            lastRotZ = DegreeRobotClass.GetAxisAngle3();
        }

        //슬라이더가 움직이면 움직일 모델링에 값 전달
        UpperUpDownSlider(upperUpDownSlider.value);
    }

    public void UpperUpDownSlider(float _moveZ)
    {
        float newValue;
        newValue = lastRotZ - _moveZ;   //전 위치 값 - 현재 위치 값

        newValue = Mathf.Abs(newValue); //절대값
        //Debug.Log("차차차::" + newValue);


        //미세 떨림 잡는 조건
        if (newValue >= 1.0f)
        {
            if (DegreeRobotClass.GetSliderMove() == false)
            {
                //현재 기준에서 회전값만큼 회전.
                //처음 있었던 위치에서 현재 이동한 위치를 뺀 만큼 회전.
                upperAxisPos.Rotate(0f, 0f, -(_moveZ - lastRotZ));
                lastRotZ = _moveZ;  //현재 위치를 마지막 위치로 저장.
            }
        }
    }
}
