using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private BoxCollider2D _col;

    private void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerDeath>().SetRespawnLocation(transform.position);
        }
    }
}
