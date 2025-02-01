using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void TransitionToGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void TransitionToEndScene()
    {
        SceneManager.LoadScene("GameEnd", LoadSceneMode.Additive);
    }

    public void TransitionToStartScene()
    {
        SceneManager.LoadScene("GameStart");
    }
}
