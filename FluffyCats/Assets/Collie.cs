using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collie : MonoBehaviour {

    private void OnTriggerEnter( Collider other ) {
        var a = other.GetComponentInParent<Asteroid>();
        if ( a != null ) {
            iTween.ShakePosition( Camera.main.gameObject, new Vector3( 0.6f, 0, 0 ), 0.4f );
            Camera.main.GetComponent<GlitchEffect>().Glitch( 0.5f );
            Spawner.StopSpawning = true;
            Killer.Die();
            var oids = GameObject.FindGameObjectsWithTag( "Asteroid" );
            foreach ( var item in oids ) {
                Destroy( item );
            }
        }
    }
}
