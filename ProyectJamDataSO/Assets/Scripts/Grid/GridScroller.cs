using UnityEngine;

public class GridScroller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _scrollSpeed = 2f;

    [Header("Start Delay")]
    [SerializeField] private float _startDelay = 3f;

    private float _timer;
    private bool _canScroll;

    private void Update()
    {
        HandleDelay();

        if (!_canScroll)
            return;

        transform.position += Vector3.down * _scrollSpeed * Time.deltaTime;
    }

    private void HandleDelay()
    {
        if (_canScroll)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _startDelay)
        {
            _canScroll = true;
        }
    }
}