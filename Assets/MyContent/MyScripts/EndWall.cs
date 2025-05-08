using Unity.Cinemachine;
using UnityEngine;

public class EndWall : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject camPos;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject Hud;
    [SerializeField] private GameObject endScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.Target.TrackingTarget = camPos.transform;
        pauseMenu.SetActive(true);
        Hud.SetActive(false);
        mainPanel.SetActive(false);
        endScreen.SetActive(true);

    }
}
