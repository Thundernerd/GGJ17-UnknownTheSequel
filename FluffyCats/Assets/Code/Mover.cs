using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public string HAxis = "Horizontal";
    public string VAxis = "Vertical";

    public float Speed;
    [Range( 0.01f, 1 )]
    public float Drag = 0.5f;

    public Vector3 Direction {
        get { return new Vector3( h, v, 0 ); }
    }

    private float v = 0;
    private float h = 0;


    private Rigidbody body;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        v = Mathf.Lerp( v, Input.GetAxis( VAxis ), Drag );
        h = Mathf.Lerp( h, Input.GetAxis( HAxis ), Drag );

        transform.position += new Vector3( h, v ) * Speed * Time.deltaTime;
    }
}
