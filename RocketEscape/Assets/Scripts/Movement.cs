using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1500f;
    [SerializeField] float rotationThrust = 500f;
    [SerializeField] AudioClip mainThruster;    
    
    Rigidbody rocketBody;
    AudioSource audioSource;  

    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();        
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
        ProcessThrust();
        ProcessRotate();
        ProcessAlignVert();
    }

    void ProcessRotate()
    {   
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal")< 0)
        {
            Rotate(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
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
            if (!audioSource.isPlaying)
            {                
                audioSource.PlayOneShot(mainThruster);
            }
            rocketBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);            
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

    }

    void ProcessAlignVert()
    {
        if(Input.GetKey(KeyCode.Joystick1Button1))
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
