using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WristAxisRotateCtrl : MonoBehaviour
{
    public Transform wirstAixsRotate; //움직일 대상
    public Transform palm;  //손목(값 받아오는 대상)

    public Slider wristRotateSlier;
    public TextMeshProUGUI wristValueText;

    float rotX, lastRotX, _x;

    public static float x;

    void Start()
    {
        //rotX = wirstAixsRotate.rotation.x;
        //lastRotX = rotX;
        lastRotX = wirstAixsRotate.rotation.x;
    }

    
    void Update()
    {
        //x = UnityEditor.TransformUtils.GetInspectorRotation(palm.transform).x;
        //palm값을 최소, 최대 범위 지정해서 그 사이에서 값이 있게 한다.
        //x = Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(palm.transform).x, -35f, 50);
        //x = Mathf.DeltaAngle(0, palm.localEulerAngles.x);
        x= Mathf.Clamp(Mathf.DeltaAngle(0, palm.localEulerAngles.x), -35f, 50);

        //if (x < 360)
        //{
        //    x -= 360;
        //}

        Debug.Log(x);
        //_x = Mathf.Lerp(0f, 90f, Mathf.InverseLerp(80, 340f, x));

        //x = Mathf.Round(x * 100) * 0.01f;   //소수점 두번째자리까지 나타냄.

        //손목 움직이는 값을 받아서 슬라이더와 텍스터에 보낸다.
        wristRotateSlier.value = x; 
        wristValueText.text = x.ToString();
        if (DegreeRobotClass.GetSliderMove())
        {
            wristRotateSlier.value = DegreeRobotClass.GetAxisAngle4();
            lastRotX = DegreeRobotClass.GetAxisAngle4();
        }

        //손목 움직이는대로 값 전달
        WristRotateSlider(wristRotateSlier.value);

    }

    public void WristRotateSlider(float _moveX)
    {
        float newValue;
        newValue = lastRotX - _moveX;   //전 위치 값 - 현재 위치 값

        newValue = Mathf.Abs(newValue); //절대값
        //Debug.Log("차차차::" + newValue);

        //미세 떨림 잡는 조건
        if (newValue >= 0.9f)
        {
            if (DegreeRobotClass.GetSliderMove() == false)
            {
                wirstAixsRotate.Rotate(0f, 0f, -(_moveX - lastRotX));
                lastRotX = _moveX;
            }
        }
    }
}
