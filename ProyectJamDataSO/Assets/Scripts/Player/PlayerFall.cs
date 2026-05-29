using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFall : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;

    [SerializeField] private float _groundRadius = 0.15f;

    [SerializeField] private LayerMask _groundLayers;

    [Header("Fall Visual")]
    [SerializeField] private float _fallMoveSpeed = 3f;

    [SerializeField] private float _fallShrinkSpeed = 2f;

    private bool _isFalling;

    private PlayerController _playerController;
    private Rigidbody2D _rb;

    public bool IsFalling => _isFalling;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isFalling)
        {
            UpdateFall();
            return;
        }

        CheckGround();
    }

    private void CheckGround()
    {
        bool grounded = Physics2D.OverlapCircle(
            _groundCheck.position,
            _groundRadius,
            _groundLayers
        );

        if (!grounded)
        {
            StartFall();
        }
    }

    private void StartFall()
    {
        _isFalling = true;

        _rb.linearVelocity = Vector2.zero;

        _rb.simulated = false;
    }

    private void UpdateFall()
    {
        transform.position += Vector3.down * _fallMoveSpeed * Time.deltaTime;

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            Vector3.zero,
            _fallShrinkSpeed * Time.deltaTime
        );

        if (transform.localScale.x <= 0.05f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            _groundCheck.position,
            _groundRadius
        );
    }
}