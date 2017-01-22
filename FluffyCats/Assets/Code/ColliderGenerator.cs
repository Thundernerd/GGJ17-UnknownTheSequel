using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGenerator : MonoBehaviour {

    public GameObject Prefab;

    // Use this for initialization
    void Start() {
        var c = Camera.main;
        var ch = c.orthographicSize + 0.5f;
        var cw = ( 16f / 9 ) * c.orthographicSize + 0.5f;

        var g = Instantiate( Prefab, new Vector3(), Quaternion.identity ) as GameObject;
        g.transform.position = new Vector3( cw, 0 );
        g.GetComponent<BoxCollider>().size = new Vector3( 1, ch * 2 );
        var p = g.GetComponent<Pusher>();
        p.Horizontal = true;
        p.Side = Sides.Right;

        g = Instantiate( Prefab, new Vector3(), Quaternion.identity ) as GameObject;
        g.transform.position = new Vector3( -cw, 0 );
        g.GetComponent<BoxCollider>().size = new Vector3( 1, ch * 2 );
        p = g.GetComponent<Pusher>();
        p.Horizontal = true;
        p.Side = Sides.Left;

        g = Instantiate( Prefab, new Vector3(), Quaternion.identity ) as GameObject;
        g.transform.position = new Vector3( 0, ch );
        g.GetComponent<BoxCollider>().size = new Vector3( cw * 2, 1 );
        p = g.GetComponent<Pusher>();
        p.Horizontal = false;
        p.Side = Sides.Top;

        g = Instantiate( Prefab, new Vector3(), Quaternion.identity ) as GameObject;
        g.transform.position = new Vector3( 0, -ch );
        g.GetComponent<BoxCollider>().size = new Vector3( cw * 2, 1 );
        p = g.GetComponent<Pusher>();
        p.Horizontal = false;
        p.Side = Sides.Bottom;
    }
}
