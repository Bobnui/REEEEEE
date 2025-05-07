using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite unlitSprite;
    [SerializeField] private Sprite litSprite;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.sprite = litSprite;
        if (collision.gameObject.TryGetComponent(out PlayerDeath death))
        {
            death.SetRespawnLocation(transform.position, this);
        }
    }
}
