using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float Distance = 1.0f;
    public float Height = 0.1f;
    public float Heading = 0f;

    public Camera Camera;

    void Update()
    {



        if (Input.GetMouseButton(1))
        {
            Heading += Input.GetAxis("Mouse X")*16f;


        }

        var euler = transform.rotation.eulerAngles;

        euler.y = Heading;
        transform.eulerAngles = euler;

        if (!Camera) return;




    }
}
