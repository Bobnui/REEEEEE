using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private Material deathMaterial;
    [SerializeField, Range(0, 4)] public float dissolveSpeed = 1;
    private enum objectTypes {Cannonball, DroppedObject}


    [SerializeField] private bool destroyOnTrigger = false;
    [Header("Only relevant if destroyed on trigger")]
    [SerializeField] private objectTypes objectType;
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
        switch(objectType)
            {
                case objectTypes.Cannonball:
                    SFXManager.Instance.PlayClip("cannonimpact", transform, 1, true);
                    Destroy(gameObject);
                    break;
                case objectTypes.DroppedObject:
                    SFXManager.Instance.PlayClip("dropperimpact", transform, 1, true);
                    Destroy(gameObject);
                    break;
            }
        }
    }
