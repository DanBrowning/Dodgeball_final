using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball :MonoBehaviour {

    

    public float m_force = 0.0f;
    public Transform m_desiredDestination;

    public Transform m_redPost;
    public Transform m_bluePost;

    public bool canHold = true;
    public GameObject item;
    public Transform Holding;

    private Rigidbody m_rb = null;

    enum EFrictionType
    {
        EFT_Static = 0,
        EFT_Dynamic,
    };

    
    float CalculateNormalForce()
    {
        return Physics.gravity.y * -1.0f * m_rb.mass;
    }

    
    float ConvertForceToAcceleration(float force,float mass)
    {
        return mass > Mathf.Epsilon ? force / mass : 0.0f;
    }

    float CalculateInitialVelocity(float finalVelocity,float acceleration,float distance)
    {
        //vf2 = vi2 + 2ad
        //vf2 - 2ad = vi2
        //vi2 = vf2 - 2ad
        //sqrt(vi2) = sqrt(vf2 - 2ad)
        return Mathf.Sqrt((finalVelocity * finalVelocity) - (2 * acceleration * distance));
    }

    // Use this for initialization
    void Start() {
        m_rb = GetComponent<Rigidbody>();
        //Collider coll = GetComponent<Collider>();
        //coll.material.dynamicFriction;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 toDestination = m_desiredDestination.position - transform.position;
            float distance = Mathf.Abs(toDestination.z);
            //Vector3 force = transform.forward * m_force;
            //m_rb.AddForce(force);
        }
    }

    private void DetermineAgentColour()
    {
        Vector3 agentPosition = transform.position;
        float distanceToRedPost = Vector3.Distance(agentPosition,m_redPost.position);
        float distanceToBluePost = Vector3.Distance(agentPosition,m_bluePost.position);
    }

    //when touches the ground, take on the tag of the colour of the ground
    //when collides with ground, switches to that colour
    //if hits agent of other colour, they have chance to catch. to catch, 1d2

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Blue (Instance)")
        {
            gameObject.tag = "Blue (Instance)";
        }
        else if (other.gameObject.tag == "Red")
        {
            gameObject.tag = "Red";
        }
    }

    public void PickUp(Transform parent)
    {
        Debug.Log("Grab");

        m_rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.position = parent.position;
        transform.parent = parent;
    }
}
