using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerDeath>().SetRespawnLocation(transform.position, gameObject.GetComponent<Checkpoint>());
        }
    }
}
