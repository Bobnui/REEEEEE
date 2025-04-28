using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Collectable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectableAmounts;
    [SerializeField, Range(0, 10)] private float rotateSpeed = 1;
    private float currentCollected;
    private void Awake()
    {
        currentCollected = 0;
    }
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0, rotateSpeed, 0, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collect();
        }
    }
    void Collect()
    {
        currentCollected += 1;
        SFXManager.Instance.PlayClip("collect", transform, 1, false);
        collectableAmounts.text = currentCollected.ToString();
        Destroy(gameObject);
    }
}
