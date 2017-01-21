using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public Colors Color;
    public float Speed;

    public static float Mod;

    private Vector3 direction;

    // Use this for initialization
    void Start() {
        Color = (Colors)Random.Range( 0, System.Enum.GetValues( typeof( Colors ) ).Length );

        switch ( Color ) {
            case Colors.Red:
                GetComponent<MeshRenderer>().material.color = new Color( 1, 0, 0 );
                break;
            case Colors.Blue:
                GetComponent<MeshRenderer>().material.color = new Color( 0, 0, 1 );
                break;
            case Colors.Yellow:
                GetComponent<MeshRenderer>().material.color = new Color( 1, 1, 0 );
                break;
        }

        var diff = -transform.position;
        var angle = Mathf.Atan2( diff.y, diff.x ) * Mathf.Rad2Deg;
        angle += Random.Range( -20, 20 );
        angle *= Mathf.Deg2Rad;
        direction.x = Mathf.Cos( angle ) * Mathf.Rad2Deg;
        direction.y = Mathf.Sin( angle ) * Mathf.Rad2Deg;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update() {
        transform.position += direction * Time.deltaTime * Speed * Mod;
    }
}
