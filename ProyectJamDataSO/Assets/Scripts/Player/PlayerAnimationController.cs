using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    // Animator parameter names
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int Speed = Animator.StringToHash("Speed");
    //private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    //private static readonly int JumpX = Animator.StringToHash("jumpX");
    //private static readonly int JumpY = Animator.StringToHash("jumpY");

    [Header("Smoothing")]
    [Tooltip("How fast the blend-tree values follow the actual input (lower = smoother).")]
    [SerializeField] private float _animationSmoothing = 10f;

    // Cached references 
    private Animator _animator;
    private PlayerController _playerController;

    // Internal state 
    private Vector2 _smoothedDir = Vector2.down;     // default face-down
    private Vector2 _lastMovDir = Vector2.down;     // last non-zero direction

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        UpdateMovementAnimation();
    }

    // Movement (Idle / Walk blend trees)
    private void UpdateMovementAnimation()
    {
        Vector2 input = _playerController.MoveInput;
        float speed = input.magnitude;

        // Keep last valid direction so Idle blend tree faces the right way
        if (input.sqrMagnitude > 0.01f)
            _lastMovDir = input.normalized;

        // Smooth the blend-tree direction
        Vector2 targetDir = speed > 0.01f ? input.normalized : _lastMovDir;
        _smoothedDir = Vector2.Lerp(_smoothedDir, targetDir,
                                    Time.deltaTime * _animationSmoothing);

        _animator.SetFloat(MoveX, _smoothedDir.x);
        _animator.SetFloat(MoveY, _smoothedDir.y);
        _animator.SetFloat(Speed, speed);
    }
}