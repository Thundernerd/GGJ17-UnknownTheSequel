using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillInTime : MonoBehaviour {

    public float seconds;

    // Use this for initialization
    IEnumerator Start() {
        yield return new WaitForSeconds( seconds );
        Destroy( gameObject );
        yield break;
    }
}
