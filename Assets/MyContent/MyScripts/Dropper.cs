using UnityEngine;
using UnityEngine.Rendering;

public class Dropper : MonoBehaviour
{
    public GameObject AcidVial;

    private float _timer;

    [SerializeField]
    private Transform barrelTransform;

    [SerializeField]
    private float spawnTime = 1;
    private void Awake()
    {
        _timer = 0;
    }
    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer >= spawnTime)
        {
            SpawnAcidVial();
            _timer = 0f;
        }
    }
    private void SpawnAcidVial()
    {
        Transform t = AcidVial.transform;
        t.position = barrelTransform.position;
        t.rotation = Quaternion.Euler(0, 0, 0);
        t.localScale = Vector3.one;

        Instantiate(AcidVial, barrelTransform.position, barrelTransform.rotation);
    }
}
