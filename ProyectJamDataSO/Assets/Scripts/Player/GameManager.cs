// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Players")]
    [SerializeField] private GameObject _playerWhite;
    [SerializeField] private GameObject _playerBlack;

    [Header("Spawn Points")]
    [SerializeField] private Transform _spawnWhite;
    [SerializeField] private Transform _spawnBlack;

    [Header("Camera")]
    [SerializeField] private GridScroller _gridScroller;
    [SerializeField] private Transform _cameraSpawn;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void OnPlayerDied()
    {
        Debug.Log("OnPlayerDied llamado");
        RespawnBoth();
        ResetCamera();
    }

    private void RespawnBoth()
    {
        Debug.Log($"Spawneando White en {_spawnWhite.position}");
        Debug.Log($"Spawneando Black en {_spawnBlack.position}");
        Respawn(_playerWhite, _spawnWhite.position);
        Respawn(_playerBlack, _spawnBlack.position);
    }

    private void Respawn(GameObject player, Vector2 position)
    {
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.transform.position = position;
        Debug.Log($"{player.name} movido a {player.transform.position}");
    }

    private void ResetCamera()
    {
        Debug.Log($"Camera antes: {_gridScroller.transform.position}");
        Debug.Log($"Camera spawn target: {_cameraSpawn.position}");
        _gridScroller.ResetTo(_cameraSpawn.position);
        Debug.Log($"Camera despues: {_gridScroller.transform.position}");
    }
}