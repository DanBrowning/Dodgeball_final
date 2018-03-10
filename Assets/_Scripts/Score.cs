﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text winner;

    public bool Red = false;
    public bool Blue = false;

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Red)
        {
            winner.gameObject.SetActive(true);
            winner.color = Color.red;
            winner.text = "The Red Team Wins!";
        }
        else if (Blue)
        {
            winner.gameObject.SetActive(true);
            winner.color = Color.blue;
            winner.text = "The Blue Team Wins!";
        }
    }
}
