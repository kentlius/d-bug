using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject scoresMenu;

    private void Update()
    {
        if (PlayerPrefs.HasKey("IsScoreBoardActive"))
        {
            int isActive = PlayerPrefs.GetInt("IsScoreBoardActive", 0);
            scoresMenu.SetActive(isActive == 1);
            mainMenu.SetActive(!(isActive == 1));
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Opening");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
