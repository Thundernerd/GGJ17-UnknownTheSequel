using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmagic : MonoBehaviour {

    public Texture2D[] Frames;
    public int CurrentIndex = 0;

    private Material mat;

    private float timer = 0;
    private float time = 1f / 30f;

    public float StartAfterXSeconds = 2f;

    public bool Running = false;

    void Start () {
        mat = GetComponent<Renderer>().material;

        Frames = Resources.LoadAll<Texture2D>( "BackgroundMagic" );
        StartIt();           
	}

    public void StartIt() {        
        CurrentIndex = 0;
        StartCoroutine( startit( StartAfterXSeconds ) );
    }

    IEnumerator startit(float seconds) {
        yield return new WaitForSeconds( seconds );

        Running = true;
    }

    float mapRange( float value, float low1, float high1, float low2, float high2 ) {
        return low2 + ( high2 - low2 ) * ( value - low1 ) / ( high1 - low1 );
    }

    void Update () {
        if(Running) {
            var mag = Sticker.maxDist;
            var value = mapRange( mag, 40, 0, 1f, 2f );

            timer += Time.deltaTime * value;
            if ( timer >= time ) {
                timer = 0;
                CurrentIndex++;
                if ( CurrentIndex >= Frames.Length ) {
                    CurrentIndex = 0;
                }
                mat.mainTexture = Frames[CurrentIndex];
            }

            
        }
    }
}
