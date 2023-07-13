using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonRobotCtrl : MonoBehaviour
{
    public Transform moveObj;

    float speed = 10f;
    float minY, maxY;

    void Start()
    {
        
    }

    
    void Update()
    {
        MoveDownModeling(); //오브젝트 내려감
        MoveUpModeling(); //오브젝트 올라감
    }


    void MoveDownModeling()
    {
        
        if(Input.GetKey(KeyCode.S))
        {
            //최소높이 이상일때 아래로 움직인다.
            if(moveObj.localPosition.y >= -.402f)
            {
                moveObj.Translate(Vector3.down * speed * Time.deltaTime);
                minY = moveObj.localPosition.y; //최소까지 갔을때 값 저장
            }
            else
            {
                //최소높이일때 더 이상 내려가지 않고 현재 위치 유지
                moveObj.localPosition = new Vector3(moveObj.localPosition.x, minY, moveObj.localPosition.z);
            }
        }
    }

    void MoveUpModeling()
    {
        if(Input.GetKey(KeyCode.W))
        {
            //최대 높이 이하일때 위로 움직인다.
            if(moveObj.localPosition.y <= 0.404f)
            {
                moveObj.Translate(Vector3.up * speed * Time.deltaTime);
                maxY = moveObj.localPosition.y; //최대까지 갔을때 값 저장
            }
            else
            {
                //최대높이일때 더 이상 올라가지 않고 현재 위치 유지
                moveObj.localPosition = new Vector3(moveObj.localPosition.x, maxY, moveObj.localPosition.z);
            }
        }
    }
}
