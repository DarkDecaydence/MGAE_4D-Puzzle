using UnityEngine;
using System.Collections;

public class InteractiveDoor : MonoBehaviour {
    public Vector3 RotationAxis;
    private float r_tween = 0;
    private bool isOpen;  // Could be changed to public and have open+close?

    private Quaternion realDirectionVector {
        get {
            return Quaternion.FromToRotation(RotationAxis, RotationAxis * 90);
        }
    }

    void Start() {
        isOpen = false;
    }

    void Update() {
        if (isOpen) {
            r_tween += Time.deltaTime * 3; // Speed factor = 3
  r_tween = Mathf.Clamp01(r_tween);
#if RealDirVector
            gameObject.transform.rotation = 
                Vector3.Lerp(gameObject.transform.rotation, realDirectionVector, r_tween);
#else
            gameObject.transform.rotation =
                Quaternion.Lerp(gameObject.transform.rotation, realDirectionVector, r_tween);
#endif
        }
    }

    void DoAction() {
        isOpen = true;
    }
}
