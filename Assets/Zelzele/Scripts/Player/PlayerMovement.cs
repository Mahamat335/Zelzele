using UnityEngine;

namespace Zelzele.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private Transform _playerBody;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private LayerMask _groundLayer; // Yüzey katmanını belirlemek için

        private Rigidbody _rb;
        private float _xRotation = 0f;
        private bool _isGrounded;
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (PlayerManager.Instance.IsMovementLocked)
            {
                return;
            }
            // Fare hareketi
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _playerBody.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

            // Zıplama girişi
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        void FixedUpdate()
        {
            if (PlayerManager.Instance.IsMovementLocked)
            {
                return;
            }
            // Hareket girişleri
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            _rb.MovePosition(_rb.position + move * _speed * Time.fixedDeltaTime);

            // Yerde olup olmadığını kontrol et
            _isGrounded = Physics.CheckSphere(transform.position, 1.0f, _groundLayer);
        }
    }
}
