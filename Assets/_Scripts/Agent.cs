using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Material _blue;
    public Material _red;
    public MeshRenderer rend;
    private Rigidbody m_rb = null;

    public float m_linearMaxSpeed = 0.0f;
    public float m_angularMaxSpeed = 0.0f;

    public float m_linearAcceleration = 0.0f;
    public float m_angularAcceleration = 0.0f;
    private int catchProb;

    public float linearSpeed
    {
        get;
        set;
    }

    public float angularSpeed
    {
        get;
        set;
    }

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<MeshRenderer>();
        m_rb = GetComponent<Rigidbody>();
        linearSpeed = 0.0f;
        angularSpeed = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Blue (Instance)")
        {
            rend.material = _blue;
        }
        else if (other.gameObject.tag == "Red")
        {
            rend.material = _red;
        }
    }

    public void MoveForwards()
    {
        m_rb.velocity = transform.forward * linearSpeed;
    }

    public void MoveBackwards()
    {
        m_rb.velocity = transform.forward * linearSpeed * -1.0f;
    }

    public void StrafeLeft()
    {
        m_rb.velocity = transform.right * linearSpeed * -1.0f;
    }

    public void StrafeRight()
    {
        m_rb.velocity = transform.right * linearSpeed;
    }

    public void TurnRight()
    {
        m_rb.angularVelocity = transform.up * angularSpeed;
    }

    public void TurnLeft()
    {
        m_rb.angularVelocity = transform.up * angularSpeed * -1.0f;
    }

    public void StopLinearVelocity()
    {
        Vector3 stopLinearVelocity = m_rb.velocity;
        stopLinearVelocity.x = 0.0f;
        stopLinearVelocity.z = 0.0f;
        m_rb.velocity = stopLinearVelocity;
    }

    public void StopAngularVelocity()
    {
        m_rb.angularVelocity = Vector3.zero;
    }

    //agents target ball of their colour tag



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != rend.material.name)
        {
            Debug.Log(collision.gameObject.tag);
            Debug.Log(rend.material.name);
            catchProb = Random.Range(1,3);
            Debug.Log(catchProb);
            if (catchProb == 1)
            {
                Destroy(gameObject);
            }
            else
            {
                Grab.instance.Pickup();
            }
        }
    }
}
