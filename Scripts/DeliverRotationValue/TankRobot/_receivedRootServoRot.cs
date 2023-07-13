using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class _receivedRootServoRot : MonoBehaviour
{
    //private RobotBaseCtrl _rootRotValue;
    // Start is called before the first frame update
    //void Start()
    //{
    //    //_rootRotValue.buttomRotateSlider
    //}

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, RobotBaseCtrl._x, 0));
        //Debug.Log("_receivedRootServoRot : " + this.transform.rotation.eulerAngles.y);
    }
}
