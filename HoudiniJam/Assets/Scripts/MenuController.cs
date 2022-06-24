using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("SCN_MAIN");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("SCN_MENU");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
