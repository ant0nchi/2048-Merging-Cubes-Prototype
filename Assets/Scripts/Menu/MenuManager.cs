using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
