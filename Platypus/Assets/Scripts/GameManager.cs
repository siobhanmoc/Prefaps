using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Menu,
    Game,
    StateCounter
}
public class GameManager : MonoBehaviour {

    public int score = 0;
    
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(score);
	}

    public void addScore(int Point)
    {
        score += Point;
    }
}
