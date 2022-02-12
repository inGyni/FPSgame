using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] float publicSensitivity = 2.5f;
    private Rigidbody playerRb;
    private float sensitivity;
    private float distanceX = 0f;

    private void Start()
    {
        sensitivity = publicSensitivity * 100;
        playerRb = GetComponentInParent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        distanceX -= mouseY;
        distanceX = Mathf.Clamp(distanceX, -90f, 90f);

        playerRb.rotation = Quaternion.Euler(playerRb.rotation.eulerAngles + Vector3.up * mouseX);

        transform.localRotation = Quaternion.Euler(distanceX, 0, 0);
    }
}
