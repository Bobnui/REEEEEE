using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// For managing overarching gameplay.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Immediately loads the scene at the specificed build index.
    /// </summary>
    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
