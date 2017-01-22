using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    public bool Horizontal;

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
