using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float lauchPower = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<MataCharacterController>().Jump(lauchPower * 10);
        }
    }
}
