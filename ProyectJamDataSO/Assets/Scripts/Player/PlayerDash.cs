// PlayerDash.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float _dashSpeed = 16f;
    [SerializeField] private float _dashDuration = 0.18f;
    [SerializeField] private float _dashCooldown = 0.5f;

    [Header("Animation")]
    // FIX #3: Renombrado para claridad — es la ventana de tiempo
    // en que IsDashing=true para la animación, no la duración del clip
    [SerializeField] private float _dashAnimWindow = 0.6f;

    private Rigidbody2D _rb;
    private PlayerController _playerController;

    private bool _isDashing;
    private bool _isAnimatingDash;
    private float _dashTimer;
    private float _dashAnimTimer;
    private float _cooldownTimer;
    private Vector2 _dashDirection;

    public bool IsDashing => _isAnimatingDash;
    public bool IsDashingPhysics => _isDashing;
    public float DashDuration => _dashDuration;

    // FIX #2: Exponemos la dirección real del dash
    public Vector2 DashDirection => _dashDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleCooldown();
        HandleAnimTimer();

        if (Input.GetKeyDown(_playerController.InputConfig.Dash))
            TryDash();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
            UpdateDash();
    }

    private void TryDash()
    {
        if (_isDashing || _cooldownTimer > 0f) return;

        Vector2 input = _playerController.MoveInput;
        if (input.sqrMagnitude <= 0.01f) return;

        StartDash(input.normalized);
    }

    private void StartDash(Vector2 direction)
    {
        _isDashing = true;
        _isAnimatingDash = true;
        _dashDirection = direction;
        _dashTimer = _dashDuration;
        _dashAnimTimer = _dashAnimWindow;
        _cooldownTimer = _dashCooldown;
    }

    private void UpdateDash()
    {
        _dashTimer -= Time.fixedDeltaTime;
        _rb.linearVelocity = _dashDirection * _dashSpeed;

        if (_dashTimer <= 0f)
        {
            _isDashing = false;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleAnimTimer()
    {
        if (!_isAnimatingDash) return;
        _dashAnimTimer -= Time.deltaTime;
        if (_dashAnimTimer <= 0f)
            _isAnimatingDash = false;
    }

    private void HandleCooldown()
    {
        if (_cooldownTimer > 0f)
            _cooldownTimer -= Time.deltaTime;
    }
}