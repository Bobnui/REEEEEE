using TMPro;
using UnityEngine;

public class EndScreenCounter : MonoBehaviour
{
    [SerializeField] private HUD hudScript;
    [SerializeField] private TextMeshProUGUI myText;
    public void UpdateEndScreen(float cum)
    {
        Debug.Log(cum);
        myText.text = cum.ToString();
    }
}
