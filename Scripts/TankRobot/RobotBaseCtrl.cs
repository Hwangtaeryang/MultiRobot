using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotBaseCtrl : MonoBehaviour
{
    public Transform lowerBase; //로봇 전체 회전시키는 제일 아래 오브젝트
    public Transform forearm;   // 어깨(받아올 값)

    public Slider buttomRotateSlider;
    public TextMeshProUGUI buttomValueText;

    float fristRotY, lastRotY, x;

    public static float _x;

    void Start()
    {
        //시작 위기 초기화
        //fristRotY = lowerBase.rotation.y;   //좌우 움직이기 위해서 Y
        //lastRotY = fristRotY;
        lastRotY = lowerBase.rotation.y;   //좌우 움직이기 위해서 Y
    }

    
    void Update()
    {
        //x = UnityEditor.TransformUtils.GetInspectorRotation(forearm.transform).x;
        //palm값을 최소, 최대범위 지정해서 그 사이에서 값이 있게 한다.
        x = Mathf.Clamp(forearm.transform.position.x * 100f, -90f, 90);
        //x = Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(forearm.transform).y, -90f, 90f);

        //-25~40값이 들어오는걸 -90~90범위로 바꾼다.
        _x = Mathf.Lerp(-90, 90, Mathf.InverseLerp(-30f, 20, x));

        //슬라이더와 텍스터에 값을 뿌려준다.
        buttomRotateSlider.value = _x;// forearm.localRotation.eulerAngles.x;
        buttomValueText.text = _x.ToString(); // palm.localRotation.eulerAngles.x.ToString();


        //슬라이더바 업데이트
        if (DegreeRobotClass.GetSliderMove())
        {
            buttomRotateSlider.value = DegreeRobotClass.GetAxisAngle1();
            lastRotY = DegreeRobotClass.GetAxisAngle1();
        }

        //슬라이더값을 움직일 오브젝트에 값을 넣어준다.
        ButtomRotateSlider(buttomRotateSlider.value);
    }

    //buttomRotateSlider 슬라이더 바가 움직이면 메서드 호출
    public void ButtomRotateSlider(float _moveY)
    {
        float newValue;
        newValue = lastRotY - _moveY;   //전 위치 값 - 현재 위치 값

        newValue = Mathf.Abs(newValue); //절대값
        //Debug.Log("차차차::" + newValue);

        //미세 떨림 잡는 조건문(숫자 조절해서 미세조절함)
        if (newValue >= 4.5f)
        {
            if (DegreeRobotClass.GetSliderMove() == false)
            {
                //Debug.Log(DegreeRobotClass.GetSliderMove());
                lowerBase.rotation = Quaternion.Euler(0f, -_moveY, 0f);
                lastRotY = _moveY;
            }
        }
    }
}
