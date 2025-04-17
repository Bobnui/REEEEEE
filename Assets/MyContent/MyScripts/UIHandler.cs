using JetBrains.Annotations;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private GameObject _gameObject;

    public void SetGameObjectActive(GameObject go)
    {
        go.SetActive(true);
    }
    public void SetGameObjectInactive(GameObject go)
    {
        go.SetActive(false);
    }
    public void TestButton(string DebugText)
    {
        Debug.Log(DebugText);
    }
}
