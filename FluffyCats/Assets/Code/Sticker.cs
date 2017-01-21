using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Sticker : MonoBehaviour {

    public AudioListener Listener;
    float[] samples;
    public static float[] currentValues;

    public AudioSource source;

    public Transform stickOne;
    public Transform stickTwo;

    public Colors Color;

    new public LineRenderer renderer;

    //public float MovementSpeed;

    public float Frequency = 1f;
    public float Amplitude = 0.2f;

    public float SineLerpPower = 1.65f;
    public float SineSpeed = 6f;

    [Header( "Speed mod" )]
    public float SpeedMod_Min1 = 40;
    public float SpeedMod_Max1 = 0;
    public float SpeedMod_Min2 = 1;
    public float SpeedMod_Max2 = 25;
    [Header( "Pitch mod" )]
    public float PitchMod_Min1 = 0;
    public float PitchMod_Max1 = 20;
    public float PitchMod_Min2 = 0;
    public float PitchMod_Max2 = 1;

    public float BlurMinimum = 0.1f;
    public float BlurMaximum = 0.8f;

    private MotionBlur blur;

    void Start() {
        Listener = GetComponent<AudioListener>();
        renderer = GetComponent<LineRenderer>();
        source = GetComponent<AudioSource>();
        blur = GetComponent<MotionBlur>();

        renderer.numPositions = 100;
        UpdateColor( Color );


        samples = new float[512];

        currentValues = new float[8];

        StartCoroutine( analyzeAudio() );
    }

    IEnumerator analyzeAudio() {
        while ( true ) {
            yield return new WaitForSeconds( 1 / 15.0f );

            AudioListener.GetSpectrumData( samples, 0, FFTWindow.BlackmanHarris );
            var count = 0;
            float diff = 0;
            for ( int i = 0; i < 8; ++i ) {
                float average = 0;

                int sampleCount = (int)Mathf.Pow( 2, i ) * 2;
                for ( int j = 0; j < sampleCount; ++j ) {
                    average += samples[count] * ( count + 1 );
                    ++count;
                }
                average /= samples.Length;
                diff = Mathf.Clamp( average * 10.0f - currentValues[i], 0, 4 );
                currentValues[i] = average * 10;
            }
        }
    }

    float mapRange( float value, float low1, float high1, float low2, float high2 ) {
        return low2 + ( high2 - low2 ) * ( value - low1 ) / ( high1 - low1 );
    }

    void audioStuff() {        
    }

    void Update() {
        audioStuff();

        if ( Input.GetButtonDown( "Fire1" ) ) {
            var c = (int)Color;
            c--;
            if ( c < 0 ) {
                c = System.Enum.GetValues( typeof( Colors ) ).Length - 1;
            }
            UpdateColor( (Colors)c );
        } else if ( Input.GetButtonDown( "Fire2" ) ) {
            var c = (int)Color;
            c++;
            if ( c > System.Enum.GetValues( typeof( Colors ) ).Length - 1 ) {
                c = 0;
            }
            UpdateColor( (Colors)c );
        }

        //if(Input.GetButtonDown("Switch")) {
        //    Debug.Log( "Switch" );
        //    stickOne.GetComponent<Mover>().SwitchMode();
        //    stickTwo.GetComponent<Mover>().SwitchMode();
        //}

        var diff = stickTwo.position - stickOne.position;
        var middle = stickOne.transform.position + diff / 2f;
        var maxDist = diff.magnitude;

        Asteroid.Mod = mapRange( maxDist, SpeedMod_Min1, SpeedMod_Max1, SpeedMod_Min2, SpeedMod_Max2 );
        source.pitch = mapRange( maxDist, PitchMod_Min1, PitchMod_Max1, PitchMod_Min2, PitchMod_Max2 );

        if(blur != null ) {
            blur.blurAmount = mapRange( maxDist, SpeedMod_Min1, SpeedMod_Max1, BlurMinimum, BlurMaximum );
        }        

        var freq = mapRange( diff.magnitude, 20, 1, 0, 2 ) * 0.5f;
        freq = Mathf.Clamp( freq, 0, 2 );
        freq *= currentValues[3] * 100;

        var step = diff / ( renderer.numPositions - 1 );
        for ( int i = 0; i < renderer.numPositions; i++ ) {
            var p = stickOne.transform.position + ( step * i );
            var normal = new Vector3( diff.y, -diff.x );
            normal.Normalize();

            var sin = Mathf.Sin( ( i * freq ) + Time.realtimeSinceStartup * SineSpeed ) * Amplitude;
            var distToMiddle = Mathf.Pow( ( middle - p ).magnitude, SineLerpPower );
            sin = Mathf.Lerp( sin, 0, ( ( 1f / maxDist ) * distToMiddle ) );
            p += normal * sin;
            renderer.SetPosition( i, p );
        }

        var r = new Ray( stickOne.position, diff.normalized );
        RaycastHit hit;
        if ( Physics.Raycast( r, out hit, diff.magnitude - 1 ) ) {
            var other = hit.collider.gameObject;
            var a = other.GetComponent<Asteroid>();
            if ( a != null ) {
                if ( a.Color == Color ) {
                    // Explooosion
                    Destroy( hit.collider.gameObject );
                } else {
                    iTween.ShakePosition( Camera.main.gameObject, new Vector3( 1, 1, 0 ), 0.25f );
                }
            }
        }
    }

    void UpdateColor( Colors color ) {
        switch ( color ) {
            case Colors.Red:
                GetComponent<LineRenderer>().material.color = new Color( 1, 0, 0 );
                break;
            case Colors.Blue:
                GetComponent<LineRenderer>().material.color = new Color( 0, 0, 1 );
                break;
            case Colors.Yellow:
                GetComponent<LineRenderer>().material.color = new Color( 1, 1, 0 );
                break;
        }
        Color = color;
    }
}