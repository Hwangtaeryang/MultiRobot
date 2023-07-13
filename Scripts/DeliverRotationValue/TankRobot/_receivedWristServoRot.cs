using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _receivedWristServoRot : MonoBehaviour
{

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, WristAxisRotateCtrl.x, 0));
        //Debug.Log("_receivedUpperServoRot : " + this.transform.rotation.eulerAngles.y);
        
    }
}
