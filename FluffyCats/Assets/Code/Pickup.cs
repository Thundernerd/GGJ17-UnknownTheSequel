using UnityEngine;

public class Pickup : MonoBehaviour {

    public AudioClip clip;
    public float Speed = 1;

    private Vector3 direction;

    // Use this for initialization
    void Start() {
        var c1 = GameObject.Find( "Cylinder" );
        var c2 = GameObject.Find( "Cylinder (1)" );

        var diff = c2.transform.position - c1.transform.position;
        var middle = c1.transform.position + diff / 2;

        direction = middle - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update() {
        transform.position += direction * Time.deltaTime * Speed * Asteroid.Mod;
    }

    public void Explode() {
        var g = new GameObject();
        var s = g.AddComponent<AudioSource>();
        s.PlayOneShot( clip );
        g.AddComponent<KillInTime>().seconds = 2;
        Destroy( gameObject );
    }
}
