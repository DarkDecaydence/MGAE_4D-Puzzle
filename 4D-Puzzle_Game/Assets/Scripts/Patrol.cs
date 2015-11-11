using UnityEngine;
using System.Collections;

public class Patrol : FourthDimension {
    public Vector4 A;
    public Vector4 B;
    private bool goingHome = false;
    private float fW;
    public float patrolTime;
    private float curTime = 0;
	// Use this for initialization
	void Start () {
        fW = (int)W;
	}
	
	// Update is called once per frame
	void Update () {
        curTime = goingHome ? curTime - Time.deltaTime : curTime + Time.deltaTime;
        float lerpTime = curTime / patrolTime;
        if (curTime > patrolTime) {
            goingHome = true;
        }
        else if (curTime < 0) {
            goingHome = false;
        }
        var result = Vector4.Lerp(A, B, lerpTime);
        gameObject.transform.position = new Vector3(result.x, result.y, result.z);
        SetFloatW(result.w);
        Debug.Log(result.w + " " + W);
        base.Update();
	}

    private void SetFloatW(float w) {
        SetW(Mathf.RoundToInt(w));
    }


}
