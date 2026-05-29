// PlayerAnimationController.cs
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int ParamIsDashing = Animator.StringToHash("IsJumping");
    private static readonly int ParamDashIndex = Animator.StringToHash("DashIndex");

    private const int DASH_DOWN = 0;
    private const int DASH_UP = 1;
    private const int DASH_LEFT = 2;
    private const int DASH_RIGHT = 3;

    private Animator _animator;
    private PlayerController _playerController;
    private PlayerDash _dashController;

    private Vector2 _lastMovDir = Vector2.down;
    private int _currentDashIndex = -1;
    private bool _wasDashing = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _dashController = GetComponent<PlayerDash>();
    }

    private void Update()
    {
        UpdateMovementAnimation();
        UpdateDashAnimation();
    }

    private void UpdateMovementAnimation()
    {
        Vector2 input = _playerController.MoveInput;
        float speed = input.magnitude;

        // FIX #1: No actualizamos _lastMovDir si estamos dasheando,
        // para que la dirección quede "congelada" al valor pre-dash
        bool dashing = _dashController != null && _dashController.IsDashing;
        if (!dashing && input.sqrMagnitude > 0.01f)
            _lastMovDir = input.normalized;

        _animator.SetFloat(MoveX, _lastMovDir.x);
        _animator.SetFloat(MoveY, _lastMovDir.y);
        _animator.SetFloat(Speed, speed);
    }

    private void UpdateDashAnimation()
    {
        if (_dashController == null) return;

        bool dashing = _dashController.IsDashing;

        if (dashing && !_wasDashing)
        {
            // FIX #2: Usamos DashDirection (la dirección real y committeada del dash)
            // en lugar de _lastMovDir que puede estar desactualizada
            int index = DashIndex(_dashController.DashDirection);

            if (index != _currentDashIndex)
            {
                _currentDashIndex = index;
                _animator.SetInteger(ParamDashIndex, index);
            }

            _animator.SetBool(ParamIsDashing, true);
        }

        if (!dashing && _wasDashing)
        {
            _animator.SetBool(ParamIsDashing, false);
            _currentDashIndex = -1; // reset para el próximo dash
        }

        _wasDashing = dashing;
    }

    private static int DashIndex(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            return dir.x >= 0 ? DASH_RIGHT : DASH_LEFT;
        else
            return dir.y >= 0 ? DASH_UP : DASH_DOWN;
    }
}