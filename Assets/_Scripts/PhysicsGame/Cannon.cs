using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour {

    

    public float shots;
    public Text remaining;
    public Text score;

    public List<Transform> targets;

    public GameObject cannonBall;
    public Transform spawner;

    public Canvas finished;
    public Canvas lost;

    public float hits;

    // Use this for initialization
    void Start()
    {
        shots = 5;
        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Instantiate(cannonBall, spawner);
            //shots--;
        }

        if (hits == 5)
        {
            Won();
        }
        else if (shots == 0)
        {
            Lost();
        }

        remaining.text = ("Shots Remaining: " + shots);
        score.text = ("Score: " + hits);
    }

    private void MoveLeft()
    {

    }

    private void MoveRight()
    {

    }

    public void Hit()
    {
        hits++;
    }

    public void Shot()
    {
        shots--;
    }

    public void Won()
    {
        finished.enabled = true;
    }

    public void Lost()
    {
        lost.enabled = true;
    }
}
