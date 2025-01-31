using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private IEnumerator loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return null;
    }

    public void LoadStartMenu()
    {
        StartCoroutine(loadScene("GameStart"));
    }

    public void LoadGame()
    {
        StartCoroutine(loadScene("Game"));
    }

    public IEnumerator LoadEndMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameEnd", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
