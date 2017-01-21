using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    public bool Horizontal;

    private void OnCollisionEnter( Collision collision ) {
        if ( collision.gameObject.tag.Equals( "Player" ) ) {
            var n = collision.contacts[0].normal;
            var d = collision.gameObject.GetComponent<Mover>().Direction;
            var c = Vector3.Cross( n, d );
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
