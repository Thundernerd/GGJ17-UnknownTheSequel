using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    public bool Horizontal;
    public Sides Side;

    private Transform p1;
    private Transform p2;

    private void Start() {
        p1 = GameObject.Find( "ParentLeft" ).transform;
        p2 = GameObject.Find( "ParentRight" ).transform;
    }

    private void Update() {
        switch ( Side ) {
            case Sides.Left: {
                    if ( p1.position.x < transform.position.x ) {
                        var p = p1.position;
                        p.x = transform.position.x + 1;
                        p1.position = p;
                        p1.GetComponent<Mover>().Push( new Vector3( 1, 0, 0 ) );
                    }
                    if ( p2.position.x < transform.position.x ) {
                        var p = p2.position;
                        p.x = transform.position.x + 1;
                        p2.position = p;
                        p2.GetComponent<Mover>().Push( new Vector3( 1, 0, 0 ) );
                    }
                    break;
                }
            case Sides.Right: {
                    if ( p1.position.x > transform.position.x ) {
                        var p = p1.position;
                        p.x = transform.position.x - 1;
                        p1.position = p;
                        p1.GetComponent<Mover>().Push( new Vector3( -1, 0, 0 ) );
                    }
                    if ( p2.position.x > transform.position.x ) {
                        var p = p2.position;
                        p.x = transform.position.x - 1;
                        p2.position = p;
                        p2.GetComponent<Mover>().Push( new Vector3( -1, 0, 0 ) );
                    }
                    break;
                }
            case Sides.Top: {
                    if ( p1.position.y > transform.position.y ) {
                        var p = p1.position;
                        p.y = transform.position.y - 1;
                        p1.position = p;
                        p1.GetComponent<Mover>().Push( new Vector3( 0, -1, 0 ) );
                    }
                    if ( p2.position.y > transform.position.y ) {
                        var p = p2.position;
                        p.y = transform.position.y - 1;
                        p2.position = p;
                        p2.GetComponent<Mover>().Push( new Vector3( 0, -1, 0 ) );
                    }
                    break;
                }
            case Sides.Bottom: {
                    if ( p1.position.y < transform.position.y ) {
                        var p = p1.position;
                        p.y = transform.position.y + 1;
                        p1.position = p;
                        p1.GetComponent<Mover>().Push( new Vector3( 0, 1, 0 ) );
                    }
                    if ( p2.position.y < transform.position.y ) {
                        var p = p2.position;
                        p.y = transform.position.y + 1;
                        p2.position = p;
                        p2.GetComponent<Mover>().Push( new Vector3( 0, 1, 0 ) );
                    }
                    break;
                }
            default:
                break;
        }
    }

    private void OnTriggerEnter( Collider other ) {
        if ( other.tag.Equals( "Player" ) ) {
            var mover = other.GetComponent<Mover>();
            var dir = mover.Direction;
            if ( Horizontal ) {
                dir.x *= -1;
            } else {
                dir.y *= -1;
            }
            mover.Push( dir );
        }
    }
}
