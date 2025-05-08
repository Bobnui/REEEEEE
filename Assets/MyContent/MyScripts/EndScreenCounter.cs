using TMPro;
using UnityEngine;

public class EndScreenCounter : MonoBehaviour
{
    [SerializeField] private HUD hudScript;
    [SerializeField] private TextMeshProUGUI myText;
    public void UpdateEndScreen(float cum)
    {
        myText.text = cum.ToString();
    }
}
