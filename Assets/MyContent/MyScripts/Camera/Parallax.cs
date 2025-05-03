using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float startPos;
    private float length;

    [SerializeField] private GameObject cam;

    [SerializeField] private float parallaxEffect;

    [SerializeField]private bool shouldShift;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect; // 0 = move with camera || 1 = won't move at all
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(shouldShift )
        {
        //if background has scrolled to its bounds, offset it by its x size
        if(movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
        }
    }
}
