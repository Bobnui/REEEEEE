using UnityEngine;

public class BackgroundZepplin : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float speed;
    [SerializeField] private bool rightToLeft = false;


    private void FixedUpdate()
    {
        float temp = speed * 0.01f;
        if(rightToLeft)
        {
            temp = temp * -1f;
        }
        transform.Translate(temp, 0, 0);
    }
}
