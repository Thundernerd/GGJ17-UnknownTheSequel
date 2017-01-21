using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public string HAxis = "Horizontal";
    public string VAxis = "Vertical";

    public float Speed;
    [Range( 0.01f, 1 )]
    public float Drag = 0.5f;

    public float Radius = 7f;
    public float RSpeed = 1f;

    public Vector3 Direction {
        get { return new Vector3( h, v, 0 ); }
    }

    private float v = 0;
    private float h = 0;

    private bool beingPushed = false;
    private float pushTimer = 0;
    private Vector3 pushDirection;

    private Rigidbody body;

    public bool Mode = false;
    public Vector3 ModePosition;

    public Mover Other;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if( !Mode ) {
            if ( pushTimer > 0 ) {
                pushTimer -= Time.deltaTime;
                if ( pushTimer < 0 ) {
                    beingPushed = false;
                }
            }

            if ( beingPushed ) {
                h = Mathf.Lerp( h, 0, Drag );
                v = Mathf.Lerp( v, 0, Drag );
                transform.position += new Vector3( h, v ) * Speed * Time.deltaTime;
            } else {
                h = Mathf.Lerp( h, Input.GetAxis( HAxis ), Drag );
                v = Mathf.Lerp( v, Input.GetAxis( VAxis ), Drag );

                transform.position += new Vector3( h, v ) * Speed * Time.deltaTime;
            }
        } else {
            var pos = new Vector3( Mathf.Cos( Time.realtimeSinceStartup * RSpeed ) * Radius,
                                    Mathf.Sin( Time.realtimeSinceStartup * RSpeed ) * Radius, 0 );

            transform.position = ModePosition + pos;
        }
    }

    public void SwitchMode() {
        //Mode = !Mode;
        //if( Mode ) {
        //    ModePosition = transform.position;
        //    Radius = ( Other.transform.position - transform.position ).magnitude;
        //    ModePosition -= ( Other.transform.position - transform.position );
        //}
    }

    public void Push( Vector3 direction ) {
        h = direction.x;
        v = direction.y;
        beingPushed = true;
        pushTimer = 0.25f;
    }
}
