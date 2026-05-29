using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float _dashSpeed = 16f;
    [SerializeField] private float _dashDuration = 0.18f;
    [SerializeField] private float _dashCooldown = 0.5f;

    private Rigidbody2D _rb;
    private PlayerController _playerController;

    private bool _isDashing;

    private float _dashTimer;
    private float _cooldownTimer;

    private Vector2 _dashDirection;

    public bool IsDashing => _isDashing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleCooldown();

        if (Input.GetKeyDown(_playerController.InputConfig.Dash))
        {
            TryDash();
        }
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            UpdateDash();
        }
    }

    private void TryDash()
    {
        if (_isDashing)
            return;

        if (_cooldownTimer > 0f)
            return;

        Vector2 input = _playerController.MoveInput;

        if (input.sqrMagnitude <= 0.01f)
            return;

        StartDash(input.normalized);
    }

    private void StartDash(Vector2 direction)
    {
        _isDashing = true;

        _dashDirection = direction;

        _dashTimer = _dashDuration;

        _cooldownTimer = _dashCooldown;
    }

    private void UpdateDash()
    {
        _dashTimer -= Time.fixedDeltaTime;

        _rb.linearVelocity = _dashDirection * _dashSpeed;

        if (_dashTimer <= 0f)
        {
            StopDash();
        }
    }

    private void StopDash()
    {
        _isDashing = false;
    }

    private void HandleCooldown()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}