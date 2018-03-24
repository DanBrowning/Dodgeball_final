using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    static Game_Manager _instance = null;


    public float hits;

    // Use this for initialization
    void Awake ()
    {
        /*if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);*/
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    public int AddScore(int scoreAmount)
    {
        hits++;

        return scoreAmount;
    }

    public static Game_Manager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}
