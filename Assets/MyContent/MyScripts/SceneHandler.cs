using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
