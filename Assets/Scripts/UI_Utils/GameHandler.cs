using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Update is called once per frame
    UIManager uiManager;
    public static int result;
    public static int winner;

    private void Start()
    {
        Time.timeScale = 1.0f;
        uiManager = UIManager.instance;
    }
    void Update()
    {
        //TODO
        if(false /* replace by query to static variable gameEnd in Game.cs */)
        {
            doGameOver();
        }
    }

    public void doResign()
    {
        StartCoroutine(resignAndFade());
    }

    public IEnumerator resignAndFade()
    {
        Coroutine fading = StartCoroutine(FadeInBlackOut());
        GetComponent<Button>().interactable = false;
        
        //Game.gameEnd = true;
        result = 1;

        //TODO get whose turn it is from Game script
        //winner = Game.turn;
        winner = 0;

        uiManager.TransitionToEndScene();
        yield return fading;
        Time.timeScale = 0f;
    }

    private void doGameOver()
    {
        GetComponent<Button>().interactable = false;
        Time.timeScale = 0f;
        //TODO query gameEnd and draw from Game.cs and update result accordingly
        /*if (Game.gameEnd == true && Game.draw == true) 
        {
            result = 2;
            winner = 2;
        } else if (Game.gameEnd == true && Game.draw == false)
        {
            result = 0;
        }*/
        result = 0;

        //TODO query turn variable from Game.cs to get whose turn it was
        /*if(Game.draw == false)
        {
            winner = Game.turn;
        }*/
        winner = 0;

        uiManager.TransitionToEndScene();
    }

    private IEnumerator FadeInBlackOut()
    {
        float fadeDuration = 0.20f;
        float targetAlpha = 0.78f;
        float elapsedTime = 0f;
        GameObject panel = GameObject.Find("c_BlackOut").transform.Find("BlackOut").gameObject;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panel.GetComponent<Image>().color = new Vector4(0, 0, 0, Mathf.Lerp(0, targetAlpha , elapsedTime / fadeDuration));
            yield return null;
        }
        panel.GetComponent<Image>().color = new Vector4(0, 0, 0, targetAlpha);
    }
}
