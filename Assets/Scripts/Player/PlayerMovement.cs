using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private Rigidbody rb;
    private float xRotation = 0f;

    void Start()
    {
        // Rigidbody bileşenini al
        rb = GetComponent<Rigidbody>();

        // Fare imlecini gizle ve sabitle
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Fare hareketi
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerBody.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        // Hareket girişleri
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Rigidbody'yi hareket ettir
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }
}
