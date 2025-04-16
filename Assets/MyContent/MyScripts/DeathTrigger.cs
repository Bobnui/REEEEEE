using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private Material deathMaterial;
    [SerializeField] private bool destroyOnTrigger = false;
    [SerializeField, Range(0, 4)] public float dissolveSpeed = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerDeath deathScript = collision.gameObject.GetComponent<PlayerDeath>();
            MataCharacterController characterScript= collision.gameObject.GetComponent<MataCharacterController>();
            characterScript.SetAnimator(false);
            deathScript.SetDissolveMaterial(deathMaterial, dissolveSpeed);
        }
        if (destroyOnTrigger)
        {
            Destroy(gameObject);
        }
    }
}
