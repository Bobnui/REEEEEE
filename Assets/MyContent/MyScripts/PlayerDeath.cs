using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Material dissolveMaterial;

    private float dissolveAmount;
    private float dissolveSpeed;

    private bool shouldDissolve = false;

    private Rigidbody2D _rb;
    private RigidbodyConstraints2D defaultConstraints;
    private Material defaultMaterial;
    private MataCharacterController characterController;

    private Vector3 respawnLocation = Vector3.zero;

    private void Awake()
    {
        defaultMaterial = GetComponent<SpriteRenderer>().material;
        _rb = GetComponent<Rigidbody2D>();
        dissolveAmount = 0;
        defaultConstraints = _rb.constraints;
        characterController = GetComponent<MataCharacterController>();
    }
    private void Update()
    {
        if (shouldDissolve)
        {
            dissolveAmount += Time.deltaTime * dissolveSpeed;
            dissolveMaterial.SetFloat("_Dissolve_Amount", dissolveAmount);
            if(dissolveAmount >= 1.2)
            {
                shouldDissolve = false;
                transform. position = respawnLocation;
                Unfreeze();
                dissolveMaterial.SetFloat("_Dissolve_Amount", 0);
                dissolveAmount = 0;
            }
        }
    }
    public void SetDissolveMaterial(Material newMaterial, float disSpeed)
    {
        dissolveSpeed = disSpeed;
        GetComponent<SpriteRenderer>().material = newMaterial;
        dissolveMaterial = GetComponent <SpriteRenderer>().material;
        characterController.isDead = true;
        Die();
    }
    public void Die()
    {
        dissolveAmount = 0;
        shouldDissolve = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Unfreeze()
    {
        _rb.constraints = defaultConstraints;
        GetComponent<SpriteRenderer>().material = defaultMaterial;
        characterController.isDead = false;
        characterController.SetAnimator(true);
    }

    public void SetRespawnLocation(Vector3 newrespawn)
    {
         respawnLocation = newrespawn;
    }
}
