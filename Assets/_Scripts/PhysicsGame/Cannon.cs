using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour {

    public float shots;
    public Text remaining;
    public Text score;

    public List<Transform> targets = new List<Transform>();
    private int targetsIndex = 0;

    public GameObject cannonBall;
    public Transform spawner;

    public Canvas finished;
    public Canvas lost;

    public float hits;

    public List<BallProjectile> balls = new List<BallProjectile>();
    private int ballsIndex = 0;

    

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(cannonBall, spawner);
            //shots--;
            ThrowBall();
        }

        if (hits >= 5)
        {
            Won();
        }
        else if (shots <= 0)
        {
            Lost();
        }

        remaining.text = ("Shots Remaining: " + shots);
        score.text = ("Score: " + hits);
    }

    private void MoveLeft()
    {
        targetsIndex--;
    }

    private void MoveRight()
    {
        targetsIndex++;
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

    public void ThrowBall()
    {
        Transform target = null;
        int startingIndex = targetsIndex;

        for (int i = 0; i > targets.Count; i++)
        {
            if (targets[targetsIndex] == null)
                targetsIndex++;
            else
            {
                target = targets[targetsIndex];
                break;
            }
            if (targetsIndex > targets.Count - 1)
                targetsIndex = 0;
            if (targetsIndex == startingIndex)
                return;
        }


        balls[ballsIndex].Fire(target);
        ballsIndex++;
        shots--;
    }
}
