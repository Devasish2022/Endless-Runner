using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuOverlay;

    public void StartGame()
    {
        startMenuOverlay.SetActive(false);
        int mainLevel = SceneManager.sceneCount;
        SceneManager.LoadScene(mainLevel);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
