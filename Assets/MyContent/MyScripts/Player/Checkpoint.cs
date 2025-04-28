using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerDeath death))
        {
            death.SetRespawnLocation(transform.position, this);
        }
    }
}
