using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerLinkController : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private Transform _playerA;
    [SerializeField] private Transform _playerB;

    [Header("Link Settings")]
    [SerializeField] private float _maxDistance = 5f;
    [SerializeField] private float _pullStrength = 15f;

    private Rigidbody2D _rbA;
    private Rigidbody2D _rbB;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _rbA = _playerA.GetComponent<Rigidbody2D>();
        _rbB = _playerB.GetComponent<Rigidbody2D>();

        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = 2;
    }

    private void FixedUpdate()
    {
        LimitDistance();
        DrawLine();
    }

    private void LimitDistance()
    {
        Vector2 direction = _rbB.position - _rbA.position;

        float distance = direction.magnitude;

        if (distance <= _maxDistance)
            return;

        Vector2 normalizedDirection = direction.normalized;

        float excessDistance = distance - _maxDistance;

        Vector2 force = normalizedDirection * (excessDistance * _pullStrength);

        _rbA.linearVelocity += force;
        _rbB.linearVelocity -= force;
    }

    private void DrawLine()
    {
        _lineRenderer.SetPosition(0, _playerA.position);
        _lineRenderer.SetPosition(1, _playerB.position);
    }
}