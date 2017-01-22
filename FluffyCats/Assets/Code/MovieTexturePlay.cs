using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTexturePlay : MonoBehaviour {

    public bool Play = false;

    void Start() {
        if( Play ) {
            ( GetComponent<Renderer>().material.mainTexture as MovieTexture ).Play();
        }        
    }
}
