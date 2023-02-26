using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var gian = GameObject.Find("Gian");
        var patti = GameObject.Find("Patti");
        var tama = GameObject.Find("Tama");
        var newCamPos = (gian.transform.position + tama.transform.position + patti.transform.position)/3;
        newCamPos.z = -10;
        transform.SetPositionAndRotation(newCamPos, Quaternion.identity);
    }
}
