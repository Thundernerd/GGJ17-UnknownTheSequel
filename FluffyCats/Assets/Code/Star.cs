using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public float Speed = 5f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.back * Speed * Time.deltaTime;

        if(transform.position.z < Camera.main.transform.position.z) {
            transform.position = new Vector3( transform.position.x, transform.position.y, 0 );
        }
	}
}
