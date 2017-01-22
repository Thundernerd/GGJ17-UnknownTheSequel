using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    const double G = 0.0000000000667384;

    public Colors Color;
    public float Speed;

    public static float Mod;
    public float BlackHoleMass = 100f;

    public float strengthOfAttraction = 5.0f;

    public bool IgnoreDirection;
    public Vector3 direction;

    private Rigidbody body;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();

        if (!IgnoreDirection) {
            if ( Random.Range( 0, 1f ) < 0.2f ) {
                var c1 = GameObject.Find( "L2" );
                var c2 = GameObject.Find( "R4" );

                var diff = c2.transform.position - c1.transform.position;
                var middle = c1.transform.position + diff / 2;

                direction = middle - transform.position;
                direction.Normalize();
            } else {
                var diff = -transform.position;
                var angle = Mathf.Atan2( diff.y, diff.x ) * Mathf.Rad2Deg;
                angle += Random.Range( -20, 20 );
                angle *= Mathf.Deg2Rad;
                direction.x = Mathf.Cos( angle ) * Mathf.Rad2Deg;
                direction.y = Mathf.Sin( angle ) * Mathf.Rad2Deg;
                direction.Normalize();
            }
        }
    }

    // Update is called once per frame
    void Update() {
        transform.position += direction * Time.deltaTime * Speed * Mod;
    }
}
