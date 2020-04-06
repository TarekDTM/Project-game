using UnityEngine;

public class MouseNavigation : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    public Transform playerBody;
    public Transform eyesStanding;
    public Transform eyesCrouching;
    public float crouchTime = 1.0f;

    private Transform target;
    private float xRotation = 0f;
    private Vector3 temp;
    private float currentLerpTime;



    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        target = eyesStanding;
    }

    // Update is called once per frame
    // TODO: Comment to be removed later - LaMaSu 29/03/2020
    /*
     we perform all camera movement and rotation calculations in LateUpdate() 
     as this will ensure that the character has moved completely before the 
     camera tracks its position.
         */
    private void LateUpdate()
    {
        LookingAround();
        ChangeCameraHeightForCrouching();
    }

    private void ChangeCameraHeightForCrouching()
    {
        var targetY = target.position.y;
        var currentY = transform.position.y;
        var temp = transform.position;
        currentLerpTime += Time.deltaTime;

        if (targetY == currentY)
            return;

        if (currentLerpTime > crouchTime)
            currentLerpTime = crouchTime;

        float perc = currentLerpTime / crouchTime;
        var newY = Mathf.Lerp(currentY, targetY, perc);
        temp.y = newY;
        transform.position = temp;
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

    internal void UpdateCameraHeightTarget(bool isCrouched)
    {
        if (isCrouched)
            target = eyesCrouching;
        else
            target = eyesStanding;
        currentLerpTime = 0f;
    }
}
