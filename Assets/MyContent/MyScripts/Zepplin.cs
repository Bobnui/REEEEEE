using UnityEngine;

public class Zepplin : MonoBehaviour
{
    private MataCharacterController characterController;
    private bool isDone;
    private Rigidbody2D rigidBody;
    private GameObject player;
    private Rigidbody2D playerRigidbody;
    [SerializeField, Range(1, 10)] private float accel;
    private float timer;
    private bool goingUp;
    private Vector2 startingPos;
    private void Awake()
    {
        startingPos = transform.position;
        isDone = false;
        timer = 0f;
        goingUp = true;
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isDone)
        {
            isDone = true;
            player = collision.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            player.transform.parent = transform;
            characterController = collision.gameObject.GetComponent<MataCharacterController>();
            characterController.enabled = false;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            player.GetComponent<AnimatorProxy>().SetBoolFalse("isJumping");
            player.GetComponent<AnimatorProxy>().SetFloat("moveSpeed", 0);
        }
    }
    private void Update()
    {
        if(isDone)
        {
            float myAccel = accel * 0.05f;
            rigidBody.linearVelocity += new Vector2 (myAccel, 0);
            playerRigidbody.linearVelocity += new Vector2(myAccel, 0);
        }
        else
        {
            BobUpAndDown();
        }
    }
    private void BobUpAndDown()
    {
        if(goingUp)
        {
            transform.position += new Vector3(0, 0.001f, 0);
            if(transform.position.y - 1 >= startingPos.y)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.position -= new Vector3(0, 0.001f, 0);
            if(transform.position.y + 1 <= startingPos.y)
            {
                goingUp = true;
            }
        }
    }
}
