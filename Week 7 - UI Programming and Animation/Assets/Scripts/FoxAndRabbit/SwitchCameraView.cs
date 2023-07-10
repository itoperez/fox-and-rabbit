using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraView : MonoBehaviour
{
    public GameObject firstPersonCameraGO;
    public Camera firstPersonCamera;
    public GameObject thirdPersonCameraRig;
    public Camera thirdPersonCamera;

    private bool switchCamera;

    private void OnEnable()
    {
        //thirdPerson();
    }

    void Start()
    {
        switchCamera = true;
        //firstPerson();
        thirdPerson();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.Locked;
            //firstPersonCamera.enabled = !firstPersonCamera.enabled;
            //thirdPersonCamera.enabled = !thirdPersonCamera.enabled;            
            if (switchCamera)
            {
                //thirdPerson();
                firstPerson();
            }
            else
            {
                //firstPerson();
                thirdPerson();
            }
            switchCamera = !switchCamera;
            
        }
    }

    private void firstPerson()
    {
        //firstPersonCamera.enabled = true;
        //thirdPersonCamera.enabled = false;
        thirdPersonCameraRig.SetActive(false);
        firstPersonCameraGO.SetActive(true);

        //thirdPersonCameraRig.GetComponent<CameraMovement>().enabled = false;

        gameObject.GetComponent<FirstPersonFoxMovement>().enabled = true;
        gameObject.GetComponent<FirstPersonFoxCamera>().enabled = true;
        gameObject.GetComponent<ThirdPersonFoxMovement>().enabled = false;


    }

    private void thirdPerson()
    {
        //thirdPersonCamera.enabled = true;
        //firstPersonCamera.enabled = false;
        thirdPersonCameraRig.SetActive(true);
        firstPersonCameraGO.SetActive(false);

        //thirdPersonCameraRig.GetComponent<CameraMovement>().enabled = true;

        gameObject.GetComponent<FirstPersonFoxMovement>().enabled = false;
        gameObject.GetComponent<FirstPersonFoxCamera>().enabled = false;
        gameObject.GetComponent<ThirdPersonFoxMovement>().enabled = true;        
    }

    
}
