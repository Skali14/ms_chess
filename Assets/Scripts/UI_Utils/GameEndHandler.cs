using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //CHECKMATE = 0, RESIGNATION = 1, STALEMATE = 2

    //BLACK = 0, WHITE = 1, NONE = 2

    //self explanatory
    UIManager uiManager;
    void Start()
    {
        uiManager = UIManager.instance;
        setVariables(GameHandler.result, GameHandler.winner, 100);
    }

    public void doPlayAgain()
    {
        uiManager.TransitionToGameScene();
    }

    public void doMainMenu()
    {
        uiManager.TransitionToStartScene();
    }

    private void setVariables(int result, int winner, int score)
    {
        TMP_Text TMP_Result = GameObject.Find("Result_Text").GetComponent<TMP_Text>();
        TMP_Text TMP_ColorWon = GameObject.Find("ColorWon_Text").GetComponent<TMP_Text>();
        Image Black_Image = GameObject.Find("Black_Image").GetComponent<Image>();
        Image White_Image = GameObject.Find("White_Image").GetComponent<Image>();

        switch (result)
        {
            case 0: 
                TMP_Result.text = "CHECKMATE";
                break;
             case 1:
                TMP_Result.text = "RESIGNATION";
                break;
             case 2:
                TMP_Result.text = "STALEMATE";
                break;
        }

        switch (winner)
        {
            case 0:
                TMP_ColorWon.text = "BLACK won!";
                Black_Image.enabled = true;
                White_Image.enabled = false;
                break;
            case 1:
                TMP_ColorWon.text = "WHITE won!";
                Black_Image.enabled = false;
                White_Image.enabled = true;
                break;
        }
    }

}
