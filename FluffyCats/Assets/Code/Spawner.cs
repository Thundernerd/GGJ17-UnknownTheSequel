using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static bool doneTrial = false;

    public GameObject Prefab;
    public GameObject Pickup;
    public bool SpawnPickup = false;

    public float MinSpawnTime = 0.5f;
    public float MaxSpawnTime = 1.5f;

    public List<Vector2> Stages;
    public int CurrentStage = 0;

    public float NextStageAfterXSeconds = 10f;

    private System.DateTime coinStamp = System.DateTime.Now;

    // Use this for initialization
    void Start() {
        if ( doneTrial ) {
            StartCoroutine( SpawnObj() );
        } else {
            StartCoroutine( InitialRound() );
        }
    }

    private float timer = 0;
    void Update() {
        timer += Time.deltaTime;

        if ( timer >= NextStageAfterXSeconds ) {
            timer = 0;
            CurrentStage++;

            CurrentStage = Mathf.Clamp( CurrentStage, 0, Stages.Count - 1 );

            MinSpawnTime = Stages[CurrentStage].x;
            MaxSpawnTime = Stages[CurrentStage].y;
        }

    }

    IEnumerator InitialRound() {
        CreateObject( new Vector3( -20, 0, 0 ), Vector3.right );
        yield return new WaitForSeconds( 3 );
        CreateObject( new Vector3( 0, 15, 0 ), Vector3.down );
        yield return new WaitForSeconds( 3 );
        CreateObject( new Vector3( 20, 0, 0 ), Vector3.left );
        yield return new WaitForSeconds( 3 );
        CreateObject( new Vector3( 0, -15, 0 ), Vector3.up );

        yield return new WaitForSeconds( 10 );
        StartCoroutine( SpawnObj() );
        yield break;
    }

    IEnumerator SpawnObj() {
        var rnd = Random.Range( MinSpawnTime, MaxSpawnTime );
        yield return new WaitForSeconds( rnd );

        if ( SpawnPickup ) {
            rnd = Random.Range( 0, 1f );
            if ( rnd < .2f ) {
                var ts = new System.TimeSpan( System.DateTime.Now.Ticks - coinStamp.Ticks );
                if ( ts.Seconds > 5 ) {
                    coinStamp = System.DateTime.Now;
                    Instantiate( Pickup, GetPosition(), Quaternion.identity );
                }
            }
        }

        Instantiate( Prefab, GetPosition(), Quaternion.identity );

        StartCoroutine( SpawnObj() );
        yield break;
    }

    private Vector3 GetPosition() {
        var angle = Random.Range( 0, 360 ) * Mathf.Deg2Rad;
        var x = Mathf.Cos( angle ) * Mathf.Rad2Deg;
        var y = Mathf.Sin( angle ) * Mathf.Rad2Deg;

        var pos = new Vector3( x, y ).normalized * 25;
        pos.y = Mathf.Min( 15, Mathf.Max( -15, pos.y ) );
        return pos;
    }

    private void CreateObject( Vector3 position, Vector3 direction ) {
        var g = Instantiate( Prefab, position, Quaternion.identity ) as GameObject;
        var a = g.GetComponent<Asteroid>();
        a.direction = direction;
        a.IgnoreDirection = true;
    }
}
