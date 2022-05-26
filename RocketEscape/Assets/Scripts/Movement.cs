using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1500f;
    [SerializeField] float rotationThrust = 500f;
    [SerializeField] AudioClip mainThruster;
    [SerializeField] ParticleSystem LTPS;
    [SerializeField] ParticleSystem RTPS;
    [SerializeField] ParticleSystem MTPS;        


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


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            MTPS.Stop();
        }
    }

    void ProcessRotate()
    {   
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal")< 0)
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
        {
            RotateRight();
        }
        else
        {
            LTPS.Stop();
            RTPS.Stop();
        }
    }

    void ProcessAlignVert()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            transform.rotation = Quaternion.identity;
        }
    }

    void ApplyThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainThruster);
        }

        if (!MTPS.isPlaying)
        {
            MTPS.Play();
        }

        rocketBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void RotateRight()
    {
        Rotate(-rotationThrust);
        if (!LTPS.isPlaying)
        {
            LTPS.Play();
        }
    }

    void RotateLeft()
    {
        Rotate(rotationThrust);
        if (!RTPS.isPlaying)
        {
            RTPS.Play();
        }
    }

    void Rotate(float rotationThisFrame)
    {
        rocketBody.freezeRotation = true; //freezng rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);        
        rocketBody.freezeRotation = false;        
    }    
}
