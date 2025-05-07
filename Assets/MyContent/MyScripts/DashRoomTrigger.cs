using UnityEngine;

public class DashRoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject tileSet;
    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isActive)
        {
            tileSet.SetActive(true);
            isActive = true;
        }
    }
}
