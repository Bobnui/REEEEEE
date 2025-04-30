using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Collectable : MonoBehaviour
{
    [SerializeField] private HUD hudREF;
    [SerializeField, Range(0, 10)] private float rotateSpeed = 1;
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0, rotateSpeed, 0, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collect();
            hudREF.Collect();
        }
    }
    void Collect()
    {
        SFXManager.Instance.PlayClip("collect", transform, 1, false);
        Destroy(gameObject);
    }
}
