using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(LevelSO level)
    {
        GameManager.instance.SelectLevel(level);

        SceneManager.LoadScene(1);
    }
}
