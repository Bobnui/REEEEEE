using UnityEngine;

public class AcidVial : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D _col;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
