// GridScroller.cs
using UnityEngine;

public class GridScroller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _scrollSpeed = 2f;

    private bool _paused;

    private void Update()
    {
        if (_paused) return;
        transform.position += Vector3.up * _scrollSpeed * Time.deltaTime;
    }

    public void ResetTo(Vector3 position)
    {
        _paused = true;
        transform.position = position;
        _paused = false;
    }
}