using UnityEngine;
using System.Collections;

public class Grab :MonoBehaviour
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
            Drop();
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

    private void Drop()
    {
        if (!item)
            return;

        Debug.Log("Drop");

        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<Rigidbody>().useGravity = true;
        item = null;
        Holding.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * ThrowSpeed;

        Holding.GetChild(0).parent = null;

            canHold = true;
    }

    public static Grab instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}