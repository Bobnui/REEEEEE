using UnityEngine;

public class DissolveShow : MonoBehaviour
{
    private Material _material;

    private float dissolveBounce = 0f;

    private void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }
    private void Update()
    {
        dissolveBounce += (Time.deltaTime / 2);
        if (dissolveBounce > 1f)
        {
            dissolveBounce = 0f;
        }
        _material.SetFloat("_Dissolve_Amount", dissolveBounce);
        Debug.Log(dissolveBounce);
    }
}
