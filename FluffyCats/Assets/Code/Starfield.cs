using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {

    public GameObject StarPrefab;

    public float MinX = -18f;
    public float MaxX = 18f;

    public float MinY = 10f;
    public float MaxY = -10f;

    public int StarCount = 30;

    // Use this for initialization
    void Start () {
		for(var i = 0; i <StarCount; i++) {
            var posX = Random.Range( MinX, MaxX );
            var posY = Random.Range( MinY, MaxY );

            var star = Instantiate( StarPrefab, this.transform );
            star.transform.position = new Vector3( posX, posY, 0 );
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
