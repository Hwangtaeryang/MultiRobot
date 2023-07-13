using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _receivedUpperServoRot : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}
    
    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, UpperAxisCtrl._x, 0));
        //Debug.Log("_receivedUpperServoRot : " + this.transform.rotation.eulerAngles.y);
    }
}
