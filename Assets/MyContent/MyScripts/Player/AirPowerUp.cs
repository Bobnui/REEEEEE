using UnityEngine;

public class AirPowerUp : MonoBehaviour
{
    private MataCharacterController characterController;

    [SerializeField] private bool resetHover = false;
    [SerializeField, Range(0, 1)] private int addedAirJumps = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            characterController = collision.GetComponent<MataCharacterController>();
            characterController.ResetAirMobility(resetHover, addedAirJumps);
            Destroy(gameObject);
        }
    }
}
