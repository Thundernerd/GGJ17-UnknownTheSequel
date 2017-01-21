using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour {
	
	void Start () {
          
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Asteroid") {
            Destroy( col.gameObject );

            iTween.ScaleAdd( gameObject, iTween.Hash( "amount", new Vector3( 1, 1, 1 ), "time", 0.5f, "easetype", iTween.EaseType.easeInOutBack ) );
        }
    }
}
