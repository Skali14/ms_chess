using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void doExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
