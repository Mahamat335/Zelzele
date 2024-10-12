using UnityEngine;

namespace Zelzele.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private Transform _playerBody;

        private Rigidbody _rb;
        private float _xRotation = 0f;

        void Start()
        {
            // Rigidbody bileşenini al
            _rb = GetComponent<Rigidbody>();

            // Fare imlecini gizle ve sabitle
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            // Fare hareketi
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _playerBody.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        void FixedUpdate()
        {
            // Hareket girişleri
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            // Rigidbody'yi hareket ettir
            _rb.MovePosition(_rb.position + move * _speed * Time.fixedDeltaTime);
        }
    }
}
