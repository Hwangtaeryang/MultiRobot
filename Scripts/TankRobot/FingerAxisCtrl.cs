using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FingerAxisCtrl : MonoBehaviour
{
    //움직일 대상
    public Transform fingerAxis;
    public Transform fingerAxis2;

    public Transform finger;    //두번째 손가락(값 받아올 대상)
    public Transform finger2;

    public Slider fingerRotSlider;
    public TextMeshProUGUI fingerValueText;

    float rotX, lastRotX, x;

    public static float _x;

    void Start()
    {
        //rotX = fingerAxis.rotation.x;
        //lastRotX = rotX;
        //초기값 세팅
        lastRotX = fingerAxis.rotation.x;


    }

    
    void Update()
    {
        //엄지손가락과 검지손가락 사이 거리를 구한다.(거리가 멀어지면 집게 벌리고, 거리가 좁혀지면 집게를 닫는다.)
        //움직이는 범위가 소수점에서 왔다갔다해서 100곱해서 크기를 키운다.
        x = Vector3.Distance(finger.transform.localPosition*100f, finger2.transform.localPosition*100f);

        //-0.8~12사이에 들어오는 값을 슬라이더 -20~20값 범위로 변경
        _x = Mathf.Lerp(-20, 20, Mathf.InverseLerp(-0.8f, 12, x));

        //손가락 움직이는 값을 슬라이더와 텍스터에 보낸다.
        fingerRotSlider.value = _x;
        fingerValueText.text = _x.ToString();
        

        if (DegreeRobotClass.GetSliderMove())
        {
            fingerRotSlider.value = DegreeRobotClass.GetAxisAngle5();
            lastRotX = DegreeRobotClass.GetAxisAngle5();
        }

        //슬라이더값을 움직일 오브젝트에 값을 넣어준다.
        FingerRotSlider(fingerRotSlider.value);
    }


    public void FingerRotSlider(float _moveX)
    {
        if(DegreeRobotClass.GetSliderMove() == false)
        {
            //집게 같은 힘으로 같은 각을 움직여야하기 때문에 값을 반대로 줌
            fingerAxis.Rotate(0f, 0f, -(_moveX - lastRotX));
            fingerAxis2.Rotate(0f, 0f, (_moveX - lastRotX));
            lastRotX = _moveX;
        }
    }
}
