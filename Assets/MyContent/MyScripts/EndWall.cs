using Unity.Cinemachine;
using UnityEngine;

public class EndWall : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject camPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.Target.TrackingTarget = camPos.transform;
    }
}
