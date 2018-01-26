using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Inspector values
    [SerializeField] private Animator _animator;
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 3.5f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _jumpSpeed = 7.5f;
    [SerializeField] private float _stickToGroundForce = 5f;

    // Private variables
    private CharacterController _characterController = null;
    private bool _isRunning;
    private bool _jumpButtonPressed;
    private bool _isJumping;
    private Vector2 _inputVector;
    private Vector3 _moveDirection;
    private Interactable _target;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController.isGrounded)
            _isJumping = false;

        _isRunning = Input.GetButton("Sprint");
        
        if (!_jumpButtonPressed && !_isJumping)
            _jumpButtonPressed = Input.GetButtonDown("Jump");

        if (Input.GetButtonDown("Interact") && _target != null)
        {
            _target.Interact();
            // Interact with target
        }

        // Get target
        RaycastHit target;
        if (Physics.BoxCast(transform.position, new Vector3(1, 2, 2), transform.forward, out target, Quaternion.identity, 2f))
        {
            _target = target.transform.gameObject.GetComponent<Interactable>();
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Rotation");
        float speed = _isRunning ? _runSpeed : _walkSpeed;
        _inputVector = new Vector2(horizontal, vertical);

        if (_inputVector.sqrMagnitude > 1)
            _inputVector.Normalize();

        Vector3 desiredMove = transform.forward * _inputVector.y + transform.right * _inputVector.x;

        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, _characterController.radius, Vector3.down, out hitInfo, _characterController.height / 2f, 1))
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        _moveDirection.x = desiredMove.x * speed;
        _moveDirection.z = desiredMove.z * speed;

        if (_characterController.isGrounded)
        {
            _moveDirection.y = -_stickToGroundForce;
            if (_jumpButtonPressed)
            {
                _moveDirection.y = _jumpSpeed;
                _jumpButtonPressed = false;
                _isJumping = true;
                //_animator.SetBool("IsJumping", true);
                _animator.SetTrigger("Jump");
            }
            else
                _animator.SetBool("IsJumping", false);
        }
        else
            _moveDirection += Physics.gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.fixedDeltaTime);
        _animator.SetFloat("Speed", _inputVector.magnitude);
        transform.Rotate(Vector3.up, rotation * _rotationSpeed * Time.deltaTime);
    }
}
