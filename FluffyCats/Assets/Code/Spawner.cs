using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Prefab;

    public float MinSpawnTime = 0.5f;
    public float MaxSpawnTime = 1.5f;

    public List<Vector2> Stages;
    public int CurrentStage = 0;

    public float NextStageAfterXSeconds = 10f;

    // Use this for initialization
    void Start() {
        StartCoroutine( SpawnObj() );
    }

    private float timer = 0;
    void Update() {
        timer += Time.deltaTime;

        if(timer >= NextStageAfterXSeconds) {
            timer = 0;
            CurrentStage++;

            CurrentStage = Mathf.Clamp( CurrentStage, 0, Stages.Count - 1 );

            MinSpawnTime = Stages[CurrentStage].x;
            MaxSpawnTime = Stages[CurrentStage].y;
        }

    }

    IEnumerator SpawnObj() {
        var rnd = Random.Range( MinSpawnTime, MaxSpawnTime );
        yield return new WaitForSeconds( rnd );

        var angle = Random.Range( 0, 360 ) * Mathf.Deg2Rad;
        var x = Mathf.Cos( angle ) * Mathf.Rad2Deg;
        var y = Mathf.Sin( angle ) * Mathf.Rad2Deg;

        var pos = new Vector3( x, y ).normalized * 25;
        pos.y = Mathf.Min( 15, Mathf.Max( -15, pos.y ) );

        Instantiate( Prefab, pos, Quaternion.identity );

        StartCoroutine( SpawnObj() );
        yield break;
    }
}
