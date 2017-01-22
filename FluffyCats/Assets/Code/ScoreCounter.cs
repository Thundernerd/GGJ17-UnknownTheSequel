using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    private Text text;

    private float score = 0;

	void Start () {
        text = GetComponent<Text>();
	}

    float mapRange( float value, float low1, float high1, float low2, float high2 ) {
        return low2 + ( high2 - low2 ) * ( value - low1 ) / ( high1 - low1 );
    }

    public void AddScore(float value) {
        var scale = mapRange( value, 0, 40, 0.5f, 1.8f );
        transform.localScale = new Vector3( scale, scale, 1 );

        score += value / 10;
               
        text.text = ((int)score).ToString();
    }

    bool bumping = false;
    IEnumerator bump() {
        if(bumping) {
            yield return null;
        } else {
            bumping = true;
        }

        var upTime = 0.1f;
        var downTime = 0.1f;
        var waitTime = 0.5f;

        //TODO scale by score

        var amount = 0.1f;

        iTween.ScaleAdd( gameObject, iTween.Hash( "amount", new Vector3( amount, amount ), "time", upTime, "easetype", iTween.EaseType.easeOutBack ) );
        yield return new WaitForSeconds( upTime );
        iTween.ScaleAdd( gameObject, iTween.Hash( "amount", new Vector3( -amount, -amount ), "time", downTime, "easetype", iTween.EaseType.linear ) );
        yield return new WaitForSeconds( downTime + waitTime );

        bumping = false;
    }

	
	void Update () {
		
	}
}
