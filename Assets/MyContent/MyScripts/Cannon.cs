using UnityEngine;
using UnityEngine.Rendering;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;

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
        Instantiate(projectile, barrelTransform.position, barrelTransform.rotation);
    }
}
