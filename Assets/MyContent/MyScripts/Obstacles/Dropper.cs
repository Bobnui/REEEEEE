using UnityEngine;
using UnityEngine.Rendering;

public class Dropper : MonoBehaviour
{
    public GameObject spawnedObject;

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
            SpawnObject();
            _timer = 0f;
        }
    }
    private void SpawnObject()
    {
        Transform t = spawnedObject.transform;
        t.position = barrelTransform.position;
        t.rotation = Quaternion.Euler(0, 0, 0);
        t.localScale = Vector3.one;

        SFXManager.Instance.PlayClip("dropperfire", transform, 1, true);
        Instantiate(spawnedObject, barrelTransform.position, barrelTransform.rotation);
    }
}
