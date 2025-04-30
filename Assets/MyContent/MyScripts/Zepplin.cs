using UnityEngine;

public class Zepplin : MonoBehaviour
{
    private MataCharacterController characterController;
    private bool isDone;
    private void Awake()
    {
        isDone = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isDone)
        {
            isDone = true;
            characterController = collision.gameObject.GetComponent<MataCharacterController>();
            characterController.enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            collision.gameObject.GetComponent<AnimatorProxy>().SetBoolFalse("isJumping");
            collision.gameObject.GetComponent<AnimatorProxy>().SetFloat("moveSpeed", 0);
        }
    }
}
