using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Material dissolveMaterial;

    private float dissolveAmount;
    [SerializeField, Range(0, 1)] private float dissolveSpeed = 1;

    private bool shouldDissolve = false;

    private Rigidbody2D _rb;

    private void Awake()
    {
        dissolveMaterial = GetComponent<SpriteRenderer>().material;
        _rb = GetComponent<Rigidbody2D>();
        dissolveAmount = 0;
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
                transform. position = Vector3.zero;
                Unfreeze();
                dissolveMaterial.SetFloat("_Dissolve_Amount", 0);
                dissolveAmount = 0;
            }
        }
    }
    public void SetDissolveMaterial(Material newMaterial)
    {
        GetComponent<SpriteRenderer>().material = newMaterial;
        dissolveMaterial = GetComponent <SpriteRenderer>().material;
    }
    public void Die()
    {
        dissolveAmount = 0;
        shouldDissolve = true;
        _rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void Unfreeze()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
