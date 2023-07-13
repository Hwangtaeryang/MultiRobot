using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RobotController : MonoBehaviour
{
    public Transform bottomObj; // 제일 밑 좌우 받침
    public Transform lowerAxisObj;  //밑 아래위로 
    public Transform upperAxisObj;  //중간 아래위로
    public Transform wristRotateAxisObj;    //손목 좌우(회저)
    public Transform wristUpDownAxisObj;    //손몬 아래위

    public Slider sliderBottom;
    public Slider sliderLower;
    public Slider sliderUpper;
    public Slider sliderWristRotate;
    public Slider sliderWristUpDown;

    bool bottomMove;

    Vector3 currentlyLower;
    Quaternion currentlyLowerQuat;



    void Start()
    {
        bottomMove = false;
    }

    
    void Update()
    {
        //wristRotateAxisObj.rotation = Quaternion.Euler(wristRotateAxisObj.rotation.x, sliderWristRotate.value, wristRotateAxisObj.rotation.z);
        //wristUpDownAxisObj.rotation = Quaternion.Euler(sliderWristUpDown.value, wristUpDownAxisObj.rotation.y, wristUpDownAxisObj.rotation.z);
        //Debug.Log("BottomPos" + bottomObj.transform.position);
        //Debug.Log("Bottom" + bottomObj.transform.rotation);
        //Debug.Log("LowerPos" + lowerAxisObj.transform.position);
        //Debug.Log("Lower" + lowerAxisObj.transform.rotation);

        if(bottomMove)
        {
            currentlyLower = lowerAxisObj.transform.position;
            currentlyLowerQuat = lowerAxisObj.transform.rotation;
        }
    }

    public void BottomRotateSlider()
    {
        // Debug.Log("Bottom"+sliderBottom.value);
        bottomMove = true;
        bottomObj.rotation = Quaternion.Euler(bottomObj.rotation.x, sliderBottom.value, bottomObj.rotation.z);
    }

    public void LowerAxisSlidr()
    {
        //Debug.Log("Lower"+sliderLower.value);
        lowerAxisObj.position = currentlyLower;
        lowerAxisObj.rotation = Quaternion.Euler(sliderLower.value, bottomObj.rotation.y, bottomObj.rotation.z);
    }

    public void UpperAxisSlider()
    {
        Debug.Log(sliderUpper.value);
        upperAxisObj.rotation = Quaternion.Euler(sliderUpper.value, upperAxisObj.rotation.y, upperAxisObj.rotation.z);
    }

    public void WristRotateSilder()
    {
        Debug.Log(sliderUpper.value);
        wristRotateAxisObj.rotation = Quaternion.Euler(wristRotateAxisObj.rotation.x, sliderWristRotate.value, wristRotateAxisObj.rotation.z);
    }

    public void WristUpDownSlider()
    {
        Debug.Log(sliderUpper.value);
        wristUpDownAxisObj.rotation = Quaternion.Euler(sliderWristUpDown.value, wristUpDownAxisObj.rotation.y, wristUpDownAxisObj.rotation.z);
    }
}
