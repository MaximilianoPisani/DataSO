using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapActivatorTrigger : MonoBehaviour
{
    [Header("Tilemaps a activar")]
    [SerializeField] private Tilemap[] _tilemapsToEnable;

    [Header("Tilemaps a desactivar (opcional)")]
    [SerializeField] private Tilemap[] _tilemapsToDisable;

    [Header("Configuración")]
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private bool _triggerOnce = true;

    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[TilemapTrigger] Tocado por: {other.gameObject.name} | Tag: {other.tag}");

        if (_triggered && _triggerOnce) return;
        if (!other.CompareTag(_playerTag)) return;

        Activate();
    }

    private void Activate()
    {
        Debug.Log("[TilemapTrigger] Activando tilemaps!");
        if (_triggerOnce) _triggered = true;

        foreach (var tilemap in _tilemapsToEnable)
            if (tilemap != null) tilemap.gameObject.SetActive(true);

        foreach (var tilemap in _tilemapsToDisable)
            if (tilemap != null) tilemap.gameObject.SetActive(false);
    }
}