using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour {

    #region Variables
    public enum Streak_Focus{Red,Blue,Purple,Green,Yellow}
    #endregion
    [Header("Text Objects")]
    #region MenuVariables
    public TMP_Text StartText;
    public TMP_Text Score;
    [SerializeField] public Streak_Focus  StreakFocus;

    #endregion
    public int CurrentStreak;

    #region CurrentScoreStreaks
    public  int CurrentStreak_Blue;
    public  int CurrentStreak_Red;
    public  int CurrentStreak_Purple;
    public  int CurrentStreak_Green;
    public  int CurrentStreak_Yellow;
    #endregion

    #region HighScoreStreaks
    private int HighScoreStreak_Blue;
    private int HighScoreStreak_Red;
    private int HighScoreStreak_Purple;
    private int HighScoreStreak_Green;
    private int HighScoreStreak_Yellow;
    #endregion

   
    void Start ()
    {
        CurrentStreak = 0;
        CurrentStreak_Blue = 0;
        CurrentStreak_Red = 0;
        CurrentStreak_Purple = 0;
        CurrentStreak_Green = 0;
        CurrentStreak_Yellow = 0;

        HighScoreStreak_Blue = PlayerPrefs.GetInt("HighScoreStreak_Blue",0);
        HighScoreStreak_Red = PlayerPrefs.GetInt("HighScoreStreak_Red",0);
        HighScoreStreak_Purple = PlayerPrefs.GetInt("HighScoreStreak_Purple",0);
        HighScoreStreak_Green = PlayerPrefs.GetInt("HighScoreStreak_Green",0);
        HighScoreStreak_Yellow = PlayerPrefs.GetInt("HighScoreStreak_Yellow",0);

    }

    void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            StartText.enabled = false;
        }



        CurrentStreakFocus();
        StreakHighScore();
        ShowScore();
    }
    void CurrentStreakFocus()
    {
        if(StreakFocus == Streak_Focus.Red)
        {
            CurrentStreak_Red = CurrentStreak;
            Score.faceColor = new Color(255, 0, 0);
        }
        else
        {
            CurrentStreak_Red = 0;
        }

        if (StreakFocus == Streak_Focus.Blue)
        {
            CurrentStreak_Blue = CurrentStreak;
            Score.faceColor = new Color(0, 0,255);

        }
        else
        {
            CurrentStreak_Blue = 0;
        }

        if (StreakFocus == Streak_Focus.Purple)
        {
            CurrentStreak_Purple = CurrentStreak;
            Score.faceColor = new Color(255, 0, 255);
        }
        else
        {
            CurrentStreak_Purple = 0;
        }

        if (StreakFocus == Streak_Focus.Green)
        {
            CurrentStreak_Green = CurrentStreak;
            Score.faceColor = new Color(0, 255, 0);

        }
        else
        {
            CurrentStreak_Green = 0;
        }

        if (StreakFocus == Streak_Focus.Yellow)
        {
            CurrentStreak_Yellow = CurrentStreak;
            Score.faceColor = new Color(255, 255, 0);

        }
        else
        {
            CurrentStreak_Yellow = 0;
        }
        if (CurrentStreak == 0)
        {
            Score.faceColor = new Color(0, 0, 0);
        }
    }
    private void StreakHighScore()
    {
        if(HighScoreStreak_Blue <= CurrentStreak_Blue)
        {
            HighScoreStreak_Blue = CurrentStreak_Blue;
            PlayerPrefs.SetInt("HighScoreStreak_Blue", HighScoreStreak_Blue);
        }
        if (HighScoreStreak_Red <= CurrentStreak_Red)
        {
            HighScoreStreak_Red = CurrentStreak_Red;
            PlayerPrefs.SetInt("HighScoreStreak_Red", HighScoreStreak_Red);
        }
        if (HighScoreStreak_Purple <= CurrentStreak_Purple)
        {
            HighScoreStreak_Purple = CurrentStreak_Purple;
            PlayerPrefs.SetInt("HighScoreStreak_Purple", HighScoreStreak_Purple);
        }
        if (HighScoreStreak_Yellow <= CurrentStreak_Yellow)
        {
            HighScoreStreak_Yellow = CurrentStreak_Yellow;
            PlayerPrefs.SetInt("HighScoreStreak_Yellow", HighScoreStreak_Yellow);
        }
        if (HighScoreStreak_Green <= CurrentStreak_Green)
        {
            HighScoreStreak_Green = CurrentStreak_Green;
            PlayerPrefs.SetInt("HighScoreStreak_Green", HighScoreStreak_Green);

        }
        PlayerPrefs.Save();


    }
    private void ShowScore()
    {
        Score.text = "Score: " + CurrentStreak.ToString();
    }
  

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene(2);
    }
}

