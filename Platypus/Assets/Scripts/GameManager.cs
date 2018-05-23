using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GameState
{
    Menu,
    Game,
    StateCounter
}
public class GameManager : MonoBehaviour {

    public int score = 0;

	public Text playerScore;
    
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(score);
		playerScore.text = "" + score.ToString();
	}

    public void addScore(int Point)
    {
        score += Point;
    }
}
