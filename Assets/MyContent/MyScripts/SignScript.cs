using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class SignScript : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private TextMeshProUGUI textAsset;
    private void Awake()
    {
        textAsset.SetText(text);
    }
}
