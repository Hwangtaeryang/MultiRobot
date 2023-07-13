using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    
    void Start()
    {
        WebCamTexture web = new WebCamTexture(742, 644, 60);
        GetComponent<MeshRenderer>().material.mainTexture = web;
        web.Play();
    }

    
    void Update()
    {
        
    }
}
