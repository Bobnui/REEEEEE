using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private Material deathMaterial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerDeath script = collision.gameObject.GetComponent<PlayerDeath>();
            script.SetDissolveMaterial(deathMaterial);
            script.Die();
        }
    }
}
