using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTexturePlay : MonoBehaviour {
    
    void Start() {
        (GetComponent<Renderer>().material.mainTexture as MovieTexture ).Play();
    }
}
