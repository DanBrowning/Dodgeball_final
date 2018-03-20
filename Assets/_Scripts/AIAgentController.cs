/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgentController : MonoBehaviour {

    //scan for specified tags within a distance
    //if something is there, move towards it

    public Agent m_agent = null;

    public float m_destinationBuffer = 2.0f;
    public float m_scanDistance = 10.0f;

    private List<Vector3> m_pathList = new List<Vector3>();

    private bool m_isRunning = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_isRunning = !m_isRunning;
        }

        if(m_isRunning)
        {
            ScanForObjects();
            MoveTowardsDestination(); 
        }
        else
        {
            m_agent.StopAngularVelocity();
            m_agent.StopLinearVelocity();
        }
    }

    bool HasDestination()
    {
        if(m_pathList.Count > 0)
        {
            Vector3 destination = m_pathList[0];
            Vector3 toDestination = destination - m_agent.transform.position;
            float distanceToDestination = toDestination.magnitude;
            if(distanceToDestination < m_destinationBuffer)
            {
                //m_pathList.Remove(destination);
                m_pathList.RemoveAt(0);
            }
        }

        return m_pathList.Count > 0;
    }

    void MoveTowardsDestination()
    {
        if(!HasDestination())
        {
            m_agent.StopAngularVelocity();
            return;
        }

        Vector3 destination = m_pathList[0];
        Vector3 toDestination = destination - m_agent.transform.position;
        float distanceToDestination = toDestination.magnitude;
        toDestination.Normalize();
        float lookAtToDestinationDot = Vector3.Dot(m_agent.transform.forward, toDestination);
        float rightToDestinationDot = Vector3.Dot(m_agent.transform.right, toDestination);
        float toDestinationAngle = Mathf.Rad2Deg * Mathf.Acos(lookAtToDestinationDot);
        
        float speed =  m_agent.m_linearMaxSpeed;
        m_agent.linearSpeed = speed;

        float angularSpeed = m_agent.m_angularMaxSpeed;
        m_agent.angularSpeed = angularSpeed;

        //how do we face our destination
        bool shouldTurnRight = rightToDestinationDot > Mathf.Epsilon;
        if (shouldTurnRight)
        {
            m_agent.TurnRight();
        }
        else
        {
            m_agent.TurnLeft();
        }
        

        if (distanceToDestination > m_destinationBuffer)
        {
            m_agent.MoveForwards();
        }
    }

    public Vector3 OnDestinationFound(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        bool isSuccess = NavMesh.CalculatePath(m_agent.transform.position, destination, NavMesh.AllAreas, path);
        if(isSuccess)
        {
            return destination - m_agent.transform.position;
        }

        return Vector3.zero;
    }

    void ScanForObjects()
    {
        //don't scan when we have somewhere to go
        if(HasDestination())
        {
            return;
        }

        Vector3 agentPosition = m_agent.transform.position;
        int layer = LayerMask.NameToLayer("Ball");
        int layerMask = 1 << layer;
        Collider[] hitColliders = Physics.OverlapSphere(agentPosition, m_scanDistance, layerMask);
        foreach(Collider hitCollider in hitColliders)
        {
            float distanceToObject = Vector3.Distance(agentPosition, hitCollider.transform.position);
            if(distanceToObject < m_destinationBuffer)
            {
                continue;
            }

            OnDestinationFound(hitCollider.transform.position);
            
            break;
        }
    }


}
*/