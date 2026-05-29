using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Input")]
    [SerializeField] private PlayerInputConfig _inputConfig;

    public Vector2 MoveInput => _moveInput;
    public PlayerInputConfig InputConfig => _inputConfig;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private PlayerDash _playerDash;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerDash = GetComponent<PlayerDash>();
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(_inputConfig.Left))
            horizontal = -1f;
        else if (Input.GetKey(_inputConfig.Right))
            horizontal = 1f;

        if (Input.GetKey(_inputConfig.Down))
            vertical = -1f;
        else if (Input.GetKey(_inputConfig.Up))
            vertical = 1f;

        _moveInput = new Vector2(horizontal, vertical).normalized;
    }

    private void Move()
    {
        if (_playerDash != null && _playerDash.IsDashing)
            return;

        Vector2 velocity = _rb.linearVelocity;

        velocity.x = _moveInput.x * _moveSpeed;
        velocity.y = _moveInput.y * _moveSpeed;

        _rb.linearVelocity = velocity;
    }
}