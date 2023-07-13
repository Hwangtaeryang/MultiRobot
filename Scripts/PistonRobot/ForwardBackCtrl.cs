using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForwardBackCtrl : MonoBehaviour
{
    public Transform armPistonObj;  //움직일 모델
    public Transform forearm; //값 받을 모델

    public Transform finger_bone;
    
    float lastPosX, z, joint0;
    float boneFirst, boneLast, forearmFirst, forearmLast;

    void Start()
    {
        lastPosX = armPistonObj.localPosition.x;
        boneFirst = finger_bone.localPosition.y;
        forearmFirst = forearm.localPosition.z;
    }

    
    void Update()
    {
        z = Mathf.Clamp(forearm.localPosition.z, -0.34f, 0.16f);

        joint0 = Mathf.Lerp(-172f, 78f, Mathf.InverseLerp(-0.34f, 0.16f, z));

        boneLast = finger_bone.localPosition.y;
        forearmLast = forearm.localPosition.z;

        //Debug.Log(Mathf.Abs(forearmFirst - forearmLast));
        if (Mathf.Abs(boneFirst - boneLast) <= 0.05f && Mathf.Abs(forearmFirst - forearmLast) >= 0.008f)
        {
            boneFirst = boneLast;
            forearmFirst = forearmLast;
            ArmForwardBackMove(joint0);
        }
        else
        {
            boneFirst = boneLast;
            forearmFirst = forearmLast;
        }
        
    }

    public void ArmForwardBackMove(float _moveX)
    {

        armPistonObj.localPosition = new Vector3(armPistonObj.localPosition.x, armPistonObj.localPosition.y, _moveX);
        lastPosX = _moveX;
        
    }
}
