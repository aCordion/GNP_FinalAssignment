using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(0, 0, 500f);
        gameObject.transform.parent = null;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 750);
        //gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 0, 500f), transform.forward);
    }

    // Update is called once per frame
    void Update () {
		
	}

}
