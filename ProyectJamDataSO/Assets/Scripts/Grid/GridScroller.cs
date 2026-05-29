using UnityEngine;

public class GridScroller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _scrollSpeed = 2f;

    private void Update()
    {
        transform.position += Vector3.up * _scrollSpeed * Time.deltaTime;
    }
}