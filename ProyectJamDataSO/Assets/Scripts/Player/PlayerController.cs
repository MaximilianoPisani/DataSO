using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Input")]
    [SerializeField] private PlayerInputConfig _inputConfig;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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

        _moveInput = new Vector2(horizontal, vertical);

        _moveInput = _moveInput.normalized;
    }

    private void Move()
    {
        _rb.linearVelocity = _moveInput * _moveSpeed;
    }
}