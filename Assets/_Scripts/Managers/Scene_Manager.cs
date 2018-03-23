using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour {

    //static Scene_Manager _instance = null;

    // Use this for initialization
    void Start ()
    {
        /*if (instance)
            DestroyImmediate(gameObject); // destroys the new Game_Manager upon scenes being loaded and keeps the old one
        else
        {
            instance = this;

            DontDestroyOnLoad(this);
        }*/
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CPU()
    {
        SceneManager.LoadScene("Dodgeball");
    }

    public void Player()
    {
        SceneManager.LoadScene("onePlayer");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /*public static Scene_Manager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }*/
}
