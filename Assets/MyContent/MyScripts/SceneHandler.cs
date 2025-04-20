using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void OpenScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
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
