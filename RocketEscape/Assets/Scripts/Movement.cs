using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1500f;
    [SerializeField] float rotationThrust = 500f;
    Rigidbody rocketBody;

    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();     
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessRotate()
    {     
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotationThrust);
        }
    }

    void Rotate(float rotationThisFrame)
    {
        rocketBody.freezeRotation = true; //freezng rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketBody.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
        {            
            rocketBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);                
        }
    }
}
