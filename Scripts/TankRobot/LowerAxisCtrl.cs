using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LowerAxisCtrl : MonoBehaviour
{
    public Transform lowerAixsPos; //움직일 대상
    public Transform forearm;   //받아올 대상(움직일 값 받아올 대상)
    
    public Slider lowerUpDownSlider;
    public TextMeshProUGUI lowerUPDownValueText;
    
    //lowerAxis 오브젝트 로테이션 X값 저장 변수.
    float rotX, lastRotX, x; 


    void Start()
    {
        //초기값 세팅
        rotX = lowerAixsPos.rotation.x;
        lastRotX = rotX;
        //lowerUpDownSlider.value = rotX;
    }

    
    void Update()
    {
        //최소, 최대값 지정함
        //x = Mathf.Clamp(UnityEditor.TransformUtils.GetInspectorRotation(forearm.transform).x, -60f, 60);
        x = Mathf.Clamp(forearm.transform.position.z * 100, -50f, 50f);

        x = Mathf.Round(x * 100) * 0.01f;   //소수점 두번째 자리까지

        //손목 움직이는 값을 받아서 슬라이더와 텍스트에 보낸다.
        lowerUpDownSlider.value = x;
        lowerUPDownValueText.text = x.ToString();   


        if (DegreeRobotClass.GetSliderMove())
        {
            //슬라이더바 업데이트
            lowerUpDownSlider.value = DegreeRobotClass.GetAxisAngle2();
            lastRotX = DegreeRobotClass.GetAxisAngle2();
        }

        //슬라이더값을 움직일 오브젝트에 값을 넣어준다.
        LowerUpDownSlider(lowerUpDownSlider.value);
    }

    public void LowerUpDownSlider(float _moveX)
    {
        if(DegreeRobotClass.GetSliderMove() == false)
        {
            //현재 기준에서 회전값만큼 회전.
            //처음 있었던 위치에서 현재 이동한 위치를 뺀 만큼 회전.
            lowerAixsPos.Rotate(0f, 0f, -(_moveX - lastRotX));
            lastRotX = _moveX; //현재 위치를 마지막 위치로 저장.
        }
    }
}
