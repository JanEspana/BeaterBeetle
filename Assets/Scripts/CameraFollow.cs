using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float camX;
    public bool isLeftHanded;
    public float mouseSensitivity = 2f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.transform.localPosition = new Vector3(0.75f, 3f, -4f);
        camX = transform.localPosition.x;
        Camera.main.transform.localRotation = Quaternion.Euler(20, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isLeftHanded)
        {
            transform.localPosition = new Vector3(-camX, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(camX, transform.localPosition.y, transform.localPosition.z);
        }*/
        CamMoving();
    }
    void CamMoving()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            player.Rotate(Vector3.up * inputX);
        }
    }
}
