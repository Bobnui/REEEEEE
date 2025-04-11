using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitTheGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();

#endif
    }
}
