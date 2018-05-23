using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using XboxCtrlrInput;
using System.IO;
using UnityEngine.SceneManagement;


enum GameState
{
    Menu,
    Game,
    StateCounter
}
public class GameManager : MonoBehaviour {

    public int score = 0;

	public Text playerScore;

    private int state = 0;

    private bool paused;

    private XboxController controller;
    
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(score);
		playerScore.text = "" + score.ToString();

        if (XCI.GetButtonDown(XboxButton.Start) && state == 0)
        {
            paused = true;
        }

        if (XCI.GetButtonDown(XboxButton.Start) && state == 1)
        {
            paused = false;
        }

        if (paused)
        {
            PauseGame();
        }

        if (!paused)
        {
            ContinueGame();
        }
    }

    public void addScore(int Point)
    {
        score += Point;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        state = 1;
        //panel stuff here
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        state = 0;
        //hide panels here
    }

    public void Exit()
    {
        GameManagerMain.curScore = score;
        //score = 0;
        SceneManager.LoadSceneAsync("Menu");
    }
}
