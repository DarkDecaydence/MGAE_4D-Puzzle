using UnityEngine;
using System.Collections;

public class ElevatorButton : Interactive {
    public Vector3 A;
    public Vector3 B;
    private bool isOn = false;
    public float travelTime;
    private float curTime = 0;
    public bool IsEnabled;

    public override void DoAction() {
        if (IsEnabled) {
            isOn = true;
        }
    }

    protected sealed override void Update () {
        if (isOn)
        {
            curTime = curTime + Time.deltaTime;
            float lerpTime = curTime / travelTime;

            var result = Vector3.Lerp(A, B, lerpTime);
            TargetObj.transform.position = new Vector3(result.x, result.y, result.z);
            base.Update();
        }
        if (TargetObj.transform.position.Equals(B)) {
            isOn = false;
            curTime = 0;
        }
    }
}
