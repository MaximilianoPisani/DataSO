// PlayerDeathHandler.cs
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerDeathHandler : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform _spawnPoint;

    [Header("Death Zone Tags")]
    [SerializeField] private string _forbiddenTag = "DeathZoneWhite";
    // White player ? le asignás "DeathZoneWhite"
    // Black player ? le asignás "DeathZoneBlack"

    private PlayerDash _dashController;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _dashController = GetComponent<PlayerDash>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(_forbiddenTag)) return;
        Debug.Log($"IsDashing: {_dashController?.IsDashing}");
        if (_dashController != null && _dashController.IsDashing) return;
        Die();
    }

    // Opcional: si el jugador se queda parado en la zona
    // (por si entra caminando lento y OnTriggerEnter no re-dispara)
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag(_forbiddenTag)) return;
        if (_dashController != null && _dashController.IsDashing) return;

        Die();
    }

    private void Die()
    {
        Debug.Log($"Die llamado | GameManager.Instance: {GameManager.Instance}");
        _rb.linearVelocity = Vector2.zero;
        GameManager.Instance.OnPlayerDied();
    }
}