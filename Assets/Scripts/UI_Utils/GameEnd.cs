using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //CHECKMATE = 0, RESIGNATION = 1, DRAW = 2
    public int result = 0;

    //BLACK = 0, WHITE = 1
    public int winner = 0;

    //self explanatory
    public int score = 0;
    void Start()
    {
        setVariables(result, winner, score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setVariables(int result, int winner, int score)
    {
        TMP_Text TMP_Result = GameObject.Find("Result_Text").GetComponent<TMP_Text>();
        TMP_Text TMP_ColorWon = GameObject.Find("ColorWon_Text").GetComponent<TMP_Text>();
        TMP_Text TMP_Score = GameObject.Find("Score_Text").GetComponent<TMP_Text>();
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
                TMP_Result.text = "DRAW";
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

        TMP_Score.text = $"Score: {score}";
    }

}
