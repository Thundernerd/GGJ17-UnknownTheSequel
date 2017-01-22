using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MusicBloom : MonoBehaviour {

    private Bloom bloom;
    
    public int Channel = 0;
    

	void Start () {
        bloom = GetComponent<Bloom>();
	}
	
	
	void Update () {
        bloom.bloomIntensity = mapRange( Sticker.currentValues[Channel], 0, 0.03f, 2, 5 );
	}

    float mapRange( float value, float low1, float high1, float low2, float high2 ) {
        return low2 + ( high2 - low2 ) * ( value - low1 ) / ( high1 - low1 );
    }
}
