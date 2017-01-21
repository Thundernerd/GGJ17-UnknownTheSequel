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

    private float scale = 1;
    private Vector3 vscale = new Vector3();

    // Use this for initialization
    void Start() {
        scale = transform.localScale.x;
        vscale.Set( scale, scale, scale );
        body = GetComponent<Rigidbody>();
    }

    float mapRange( float value, float low1, float high1, float low2, float high2 ) {
        return low2 + ( high2 - low2 ) * ( value - low1 ) / ( high1 - low1 );
    }

    // Update is called once per frame
    void Update() {
        if ( !Mode ) {
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

                var t = new Vector2( h, v );
                scale = mapRange( t.magnitude, 0, 1, 1, 1.5f );
                vscale.Set( scale, scale, scale );
                transform.localScale = vscale;

                transform.localScale = vscale;
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
