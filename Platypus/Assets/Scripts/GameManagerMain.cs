using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerMain : MonoBehaviour {

    static public int curScore = 0;
    private int HighScore;
    private int HighScore2;
    private int HighScore3;
    private int HighScore4;
    private int HighScore5;

    public Text highscore = null;
    public Text highscore2 = null;
    public Text highscore3 = null;
    public Text highscore4 = null;
    public Text highscore5 = null;

    // Use this for initialization
    void Start ()
    {
        CheckScore();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ChangeIntToString();
        
		
	}

    public void ChangeIntToString()
    {
        LoadScene();

        highscore.text = HighScore.ToString();
        highscore2.text = HighScore2.ToString();
        highscore3.text = HighScore3.ToString();
        highscore4.text = HighScore4.ToString();
        highscore5.text = HighScore5.ToString();
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("Main scene");
    }

    public void Exit()
    {
#if UNITY_EDITOR                                                                //checks if in Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;                            //if yes, then close the Editor Simulation Application, else, then quit
#else
        Application.Quit();                                                      
#endif
    }

    public void CheckScore()
    {
        LoadScore();

        if (curScore >= HighScore)
            HighScore = curScore;
        else if (curScore >= HighScore2)
            HighScore2 = curScore;
        else if (curScore >= HighScore3)
            HighScore3 = curScore;
        else if (curScore >= HighScore4)
            HighScore4 = curScore;
        else if (curScore >= HighScore5)
            HighScore5 = curScore;
        else
        { }

        SaveScore();

    }

    public void LoadScore()
    {
        PlayerPrefs.GetInt("HighScore", HighScore);
        PlayerPrefs.GetInt("HighScore2", HighScore2);
        PlayerPrefs.GetInt("HighScore3", HighScore3);
        PlayerPrefs.GetInt("HighScore4", HighScore4);
        PlayerPrefs.GetInt("HighScore5", HighScore5);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.SetInt("HighScore2", HighScore2);
        PlayerPrefs.SetInt("HighScore3", HighScore3);
        PlayerPrefs.SetInt("HighScore4", HighScore4);
        PlayerPrefs.SetInt("HighScore5", HighScore5);
    }
}
