using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour {

    private StateManager _stateManager;

    private Rigidbody m_rb = null;

    private Transform _targetBall = null;
    private Transform _targetAgent = null;

    public float m_linearMaxSpeed = 0.0f;
    public float m_angularMaxSpeed = 0.0f;

    public float m_linearAcceleration = 0.0f;
    public float m_angularAcceleration = 0.0f;
    private int catchProb;

    public bool canHold = true;
    public GameObject item;
    public Transform Holding;
    public float ThrowSpeed;

    public List<Transform> balls;
    public List<Transform> targets;

    public Material _blue;
    public Material _red;
    public MeshRenderer rend;

    public bool isOut
    {
        get;
        private set;
    }

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

    public Transform targetBall
    {
        get { return _targetBall; }
        set { _targetBall = value; }
    }

    public Transform targetAgent
    {
        get { return _targetAgent; }
        set { _targetAgent = value; }
    }

    public bool hasBall { get; set; }

    // Use this for initialization
    void Start ()
    {
        m_rb = GetComponent<Rigidbody>();

        _stateManager = new StateManager();
        _stateManager.AddState(new IdleState(this));
        _stateManager.AddState(new AttackState(this));
        _stateManager.AddState(new DefenseState(this));
        _stateManager.AddState(new RunState(this));
        _stateManager.desiredState = Definitions.StateName.Run;

        linearSpeed = 5;

        isOut = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _stateManager.desiredState = Definitions.StateName.Idle;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _stateManager.desiredState = Definitions.StateName.Attack;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _stateManager.desiredState = Definitions.StateName.Pickup;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _stateManager.desiredState = Definitions.StateName.Run;
        }

        _stateManager.Update();

        /*if (_stateManager.currentState.GetStateName() != Definitions.StateName.Run && canHold)
        {
            SwitchState(Definitions.StateName.Run);
        }*/
        int outCount = 0;
        foreach (Transform agent in targets)
            if (agent.GetComponent<AIAgent>().isOut)
                outCount++;

        if (outCount >= targets.Count)
        {
            Winning();
        }
    }

    public void MoveForward(Vector3 direction)
    {
        m_rb.velocity = direction * linearSpeed;
    }

    public void MoveBackward()
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

    public void SwitchState(Definitions.StateName stateName)
    {
        _stateManager.desiredState = stateName;
    }

    public Vector3 GetDirectionToTarget(Vector3 position, Vector3 target)
    {
        Vector3 towards = target - position;

        return towards;
    }

    public void Pickup()
    {
        if (gameObject.tag == rend.material.name)
        {
            targetBall.GetComponent<Ball>().PickUp(Holding);
            canHold = false;

            MoveForward(Vector3.zero);

            SwitchState(Definitions.StateName.Attack);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Blue (Instance)" && other.gameObject.layer != 9)
        {
            rend.material = _blue;
            gameObject.tag = "Blue (Instance)";
        }
        else if (other.gameObject.tag == "Red (Instance)" && other.gameObject.layer != 9)
        {
            rend.material = _red;
            gameObject.tag = "Red (Instance)";
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != rend.material.name && collision.gameObject.tag != "Barrier")
        {
            Debug.Log(collision.gameObject.tag);
            Debug.Log(rend.material.name);
            catchProb = Random.Range(1, 4);
            Debug.Log(catchProb);
            if (catchProb == 1)
            {
                isOut = true;
                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                this.enabled = false;
            }
            else
            {
                Pickup();
            }
        }
    }

    public void Winning()
    {
        GameObject.FindGameObjectWithTag("WinCan").GetComponentInChildren<Score>().win(GetComponent<Renderer>().material.color);
    }
}
