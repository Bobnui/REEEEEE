using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3 (0, 0, -10f);
    private float smoothing = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothing);
    }
}
