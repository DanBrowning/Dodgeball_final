using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Material _blue;
    public Material _red;
    public MeshRenderer rend;

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Blue")
        {
            Debug.Log("Blue");
            rend.material = _blue;
            
        }
        else if (other.gameObject.tag == "Red")
        {
            rend.material = _red;
            Debug.Log("red");
        }
    }
}
