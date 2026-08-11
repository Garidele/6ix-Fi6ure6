using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("BasicScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
