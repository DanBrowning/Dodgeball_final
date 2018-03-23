using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicVelocity : MonoBehaviour {

    public Vector3 m_velocity;
    private Rigidbody m_rb = null;

    public bool moveLeft;

    private float _elapsedTime;

	// Use this for initialization
	void Start ()
    {
        m_rb = GetComponent<Rigidbody>();
        Debug.Log("Starting" + m_rb.velocity);

    }

    public Vector3 GetVelocity() { return m_rb.velocity; }

	// Update is called once per frame
	void Update ()
    {
        _elapsedTime += Time.deltaTime;
        if (moveLeft)
        {
            m_rb.velocity = m_velocity;
        }
        else
        {
            m_rb.velocity = m_velocity * -1;
        }

        /*if (_elapsedTime > 1)
        {
            Debug.Log(m_velocity);
            m_velocity *= -1;
            _elapsedTime = 0;
        }*/
    }

    
}
