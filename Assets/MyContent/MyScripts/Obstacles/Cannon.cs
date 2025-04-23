using UnityEngine;
using UnityEngine.Rendering;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;

    private float _timer;

    [SerializeField]
    private Transform barrelTransform;

    [Header("Projectile Spawn Rate")]
    [SerializeField]
    private float spawnTime = 1;
    [SerializeField] private float firstShotDelay = 0;

    [Header("Projectile Trajectory")]
    [SerializeField, Range(0, 10)] private float mySpeed = 5;
    [SerializeField, Range(0, 5)] private float myGravityScale = 0f;
    private void Awake()
    {
        _timer = 0 - firstShotDelay;
    }
    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer >= spawnTime)
        {
            SpawnProjectile();
            _timer = 0f;
        }
    }
    private void SpawnProjectile()
    {
        var obj = Instantiate(projectile, barrelTransform.position, barrelTransform.rotation);
        SFXManager.Instance.PlayClip("cannonfire", transform, 1, true);
        Projectile myScript = obj.GetComponent<Projectile>();
        myScript.SetProjectileParameters(mySpeed, myGravityScale);
    }
}
