using UnityEngine;

public class GameStartHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    UIManager uiManager;
    void Start()
    {
        uiManager = UIManager.instance;
    }

    public void doStartGame()
    {
        uiManager.TransitionToGameScene();
    }

    public void doExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game!");
    }
}
