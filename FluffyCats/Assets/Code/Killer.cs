using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Killer : MonoBehaviour {

    private static Killer _;

    public Image[] Lifes;
    private int lives = 3;

    // Use this for initialization
    void Start() {
        _ = this;
    }

    public static void Die() {
        _.timesBlinked = 0;
        _.isBlinking = true;
    }

    private bool isBlinking = false;
    private int timesBlinked = 0;
    private float counter = 0.125f;

    private void Update() {
        if ( isBlinking ) {
            if ( counter > 0 ) {
                counter -= Time.deltaTime;
            }

            if ( counter < 0 ) {
                timesBlinked++;
                if ( timesBlinked < 7 ) {
                    counter = 0.125f;
                    for ( int i = 0; i < lives; i++ ) {
                        Lifes[i].enabled = !Lifes[i].enabled;
                    }
                } else {
                    isBlinking = false;
                    Lifes[lives - 1].enabled = false;
                    lives--;
                    Spawner.StopSpawning = false;
                    GameObject.FindObjectOfType<Spawner>().StartSpawning();
                }
            }
        }
    }
}
