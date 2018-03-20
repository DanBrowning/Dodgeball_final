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

   
}
