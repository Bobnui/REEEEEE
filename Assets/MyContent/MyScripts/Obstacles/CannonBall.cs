using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _col;
    [SerializeField, Range(0, 10)] private float speed = 5;
    [SerializeField, Range(0, 5)] private float gravityScale = 0f;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }
    private void Start()
    {
        _rb.gravityScale = gravityScale;
        float projectileSpeed = speed * 100f;
        _rb.AddForce(transform.up * projectileSpeed);
    }
}
