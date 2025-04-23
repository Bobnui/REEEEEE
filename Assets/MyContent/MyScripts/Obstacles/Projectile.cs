using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _col;
    public float mySpeed = 5;
    public float myGravityScale = 0f;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }
    public void SetProjectileParameters(float projectileSpeed, float projectileGravity)
    {
        mySpeed = projectileSpeed * 100;
        myGravityScale = projectileGravity;
        MoveProjectile();
    }
    private void MoveProjectile()
    {
        _rb.gravityScale = myGravityScale;
        _rb.AddForce(transform.up * mySpeed);
    }
}
