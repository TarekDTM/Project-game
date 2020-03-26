using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseNavigation : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LookingAround();
    }

    private void LookingAround()
    {
        float mouseHorizontalLook = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseVerticalLook = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseVerticalLook;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseHorizontalLook);
    }
}
