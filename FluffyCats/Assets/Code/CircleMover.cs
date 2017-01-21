using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour {

    public float Radius = 7f;
    public float Speed = 1f;

	void Start () {
		
	}
		
	void Update () {
        var pos = new Vector3( Mathf.Cos( Time.realtimeSinceStartup * Speed ) * Radius,
                                Mathf.Sin( Time.realtimeSinceStartup * Speed ) * Radius, 0 );

        transform.position = pos;
	}
}
