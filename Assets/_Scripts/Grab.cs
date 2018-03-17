using UnityEngine;
using System.Collections;

// move all to AIAgent script


public class Grab : MonoBehaviour
{
    public bool canHold = true;
    public GameObject item;
    public Transform Holding;
    public float ThrowSpeed;

    static Grab _instance = null;

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Throw();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 9)
            if (!item)
            {
                item = collider.gameObject;
                Pickup();
            }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 9)
        {
            if (canHold)
                item = null;
        }
    }


    public void Pickup()
    {
        if (!item)
            return;

        Debug.Log("Grab");
        
        item.transform.SetParent(Holding);
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.localRotation = transform.rotation;
        item.transform.position = Holding.position;

            canHold = false;
    }

    private void Throw()
    {
        if (!item)
            return;

        Debug.Log("Throw");

        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<Rigidbody>().useGravity = true;
        item = null;
        Holding.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * ThrowSpeed;

        Holding.GetChild(0).parent = null;
        {
            canHold = true;
        }

        _owner.SwitchState(Definitions.StateName.Defend);
    }

    public static Grab instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}