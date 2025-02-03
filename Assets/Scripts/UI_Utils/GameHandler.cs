using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    UIManager uiManager;
    Game game;
    public static int result;
    public static int winner;
    public Sprite white_sprite;
    public Sprite black_sprite;
    public SpriteRenderer spriteRenderer;
    public RectTransform rectTransform;

    private void Start()
    {
        game = Game.instance;
        Time.timeScale = 1.0f;
        uiManager = UIManager.instance;
    }

    private void Update()
    {
        spriteRenderer.sprite = game.IsWhiteTurn ? white_sprite : black_sprite;
        rectTransform.rotation = game.IsWhiteTurn ? new Quaternion(0, 0, 180, 0) : new Quaternion(0, 0, 0, 0);

        if (game.GameEnd)
        {
            doGameOver();
        }
    }

    public void doResign()
    {
        StartCoroutine(resignAndFade());
    }

    public void doGameOver()
    {
        StartCoroutine(gameOverAndFade());
    }

    private IEnumerator resignAndFade()
    {
        game.GameEnd = true;
        this.enabled = false;
        Coroutine fading = StartCoroutine(FadeInBlackOut());
        GetComponent<Button>().interactable = false;

        result = 1;

        if(game.IsWhiteTurn)
        {
            winner = 0;
        } else
        {
            winner = 1;
        }

        uiManager.TransitionToEndScene();
        yield return fading;
        Time.timeScale = 0f;
    }

    private IEnumerator gameOverAndFade()
    {
        this.enabled = false;
        Coroutine fading = StartCoroutine(FadeInBlackOut());
        GetComponent<Button>().interactable = false;
        if (game.GameEnd == true && game.StaleMate == true) 
        {
            result = 2;
            winner = 2;
        } else if (game.GameEnd == true && game.StaleMate == false)
        {
            result = 0;
        }

        if(game.StaleMate == false)
        {
            if (game.IsWhiteTurn)
            {
                winner = 0;
            }
            else
            {
                winner = 1;
            }
        }

        uiManager.TransitionToEndScene();
        yield return fading;
        Time.timeScale = 0f;
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
            panel.GetComponent<Image>().color = new Vector4(0, 0, 0, Mathf.Lerp(0, targetAlpha, elapsedTime / fadeDuration));
            yield return null;
        }
        panel.GetComponent<Image>().color = new Vector4(0, 0, 0, targetAlpha);
    }
}


