using UnityEngine;

public class Pickup : MonoBehaviour {

    public AudioClip clip;
    public float Speed = 1;

    private Vector3 direction;

    // Use this for initialization
    void Start() {
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
