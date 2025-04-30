using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectableAmount;
    private float collectionCounter;

    private void Awake()
    {
        collectionCounter = 0;
    }
    public void Collect()
    {
        collectionCounter += 1;
        collectableAmount.text = collectionCounter.ToString();
    }
}
