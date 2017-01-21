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

    private Vector3 direction;

    private Rigidbody body;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();

        //Color = (Colors)Random.Range( 0, System.Enum.GetValues( typeof( Colors ) ).Length );

        //switch ( Color ) {
        //    case Colors.Red:
        //        //GetComponent<MeshRenderer>().material.color = new Color( 1, 0, 0 );
        //        GetComponent<MeshRenderer>().material.SetColor( "_EmissionColor", new Color( 1, 0, 0 ) );
        //        break;
        //    case Colors.Blue:
        //        //GetComponent<MeshRenderer>().material.color = new Color( 0, 0, 1 );
        //        GetComponent<MeshRenderer>().material.SetColor( "_EmissionColor", new Color( 0, 0, 1 ) );
        //        break;
        //    case Colors.Yellow:
        //        //GetComponent<MeshRenderer>().material.color = new Color( 1, 1, 0 );
        //        GetComponent<MeshRenderer>().material.SetColor( "_EmissionColor", new Color( 1, 1, 0 ) );
        //        break;
        //}

        var diff = -transform.position;
        var angle = Mathf.Atan2( diff.y, diff.x ) * Mathf.Rad2Deg;
        angle += Random.Range( -20, 20 );
        angle *= Mathf.Deg2Rad;
        direction.x = Mathf.Cos( angle ) * Mathf.Rad2Deg;
        direction.y = Mathf.Sin( angle ) * Mathf.Rad2Deg;
        direction.Normalize();

        //body = GetComponent<Rigidbody>();
        //body.AddForce( direction * Speed, ForceMode.Impulse );
    }

    // Update is called once per frame
    void Update() {
        //Vector3 offset = -transform.position;
        //offset.z = 0;
        //float magsqr = offset.magnitude;
        //if ( magsqr > 0.01f ) {
        //    float attraction = body.mass * BlackHoleMass;
        //    body.AddForce( attraction * offset.normalized / magsqr );
            
        //}            

        transform.position += direction * Time.deltaTime * Speed * Mod;
    }
}
