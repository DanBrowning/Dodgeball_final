using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball :MonoBehaviour {

    

    public float m_force = 0.0f;
    public Transform m_desiredDestination;

    public Transform m_redPost;
    public Transform m_bluePost;

    private Rigidbody m_rb = null;

    enum EFrictionType
    {
        EFT_Static = 0,
        EFT_Dynamic,
    };

    float CalculateFrictionCoefficient(EFrictionType frictionType)
    {
        float frictionCoefficient = 0.0f;

        //determine our friction values
        Collider coll = GetComponent<Collider>();
        float ourFriction = (frictionType == EFrictionType.EFT_Static) ? coll.material.staticFriction : coll.material.dynamicFriction;
        PhysicMaterialCombine ourCombine = coll.material.frictionCombine;

        //check if we are colliding against an object
        Vector3 ourPosition = transform.position;
        float ourHeight = transform.localScale.y;
        float ourGroundBuffer = 0.25f;

        int layer = LayerMask.NameToLayer("Ball");
        int layerMask = 1 << layer;
        layerMask = ~layerMask;
        RaycastHit hit;

        if (Physics.Raycast(ourPosition,-Vector3.up,out hit,(ourHeight * 0.5f) + ourGroundBuffer,layerMask))
        {
            float hitFriction = (frictionType == EFrictionType.EFT_Static) ? hit.collider.material.staticFriction : hit.collider.material.dynamicFriction;
            PhysicMaterialCombine hitCombine = hit.collider.material.frictionCombine;
            //Average < Minimum < Multiply < Maximum
            bool isMax = hitCombine == PhysicMaterialCombine.Maximum || ourCombine == PhysicMaterialCombine.Maximum;
            bool isMultiply = hitCombine == PhysicMaterialCombine.Multiply || ourCombine == PhysicMaterialCombine.Multiply;
            bool isMin = hitCombine == PhysicMaterialCombine.Minimum || ourCombine == PhysicMaterialCombine.Minimum;
            bool isAverage = hitCombine == PhysicMaterialCombine.Average || ourCombine == PhysicMaterialCombine.Average;

            if (isMax)
            {
                frictionCoefficient = hitFriction > ourFriction ? hitFriction : ourFriction;
            }
            else if (isMultiply)
            {
                frictionCoefficient = hitFriction * ourFriction;
            }
            else if (isMin)
            {
                frictionCoefficient = hitFriction < ourFriction ? hitFriction : ourFriction;
            }
            else if (isAverage)
            {
                frictionCoefficient = (hitFriction + ourFriction) * 0.5f;
            }
        }

        return frictionCoefficient;
    }

    float CalculateNormalForce()
    {
        return Physics.gravity.y * -1.0f * m_rb.mass;
    }

    float CalculateFrictionalForce(EFrictionType frictionType)
    {
        return CalculateNormalForce() * CalculateFrictionCoefficient(frictionType);
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
            float frictionalForce = -1.0f * CalculateFrictionalForce(EFrictionType.EFT_Dynamic);
            float acceleration = ConvertForceToAcceleration(frictionalForce,m_rb.mass);
            float speed = CalculateInitialVelocity(0.0f,acceleration,distance);
            m_rb.velocity = transform.forward * speed;
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

    
}
