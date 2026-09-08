using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Debug.Log("Đã nhấn nút Exit");
        Application.Quit();
    }

    public void Setting()
    {
        SceneManager.LoadScene("SettingScence");
    }
}