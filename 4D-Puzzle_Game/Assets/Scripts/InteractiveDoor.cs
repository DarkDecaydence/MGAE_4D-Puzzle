using UnityEngine;
using System.Collections;

public class InteractiveDoor : MonoBehaviour {
    public Vector3 RotationAxis;
    private float r_tween = 0;
    private bool isOpen;  // Could be changed to public and have open+close?
    public string key;
    public float OpenTime;
    float timePassed = 0.0f;
    public float Angle;

    private Quaternion realDirectionVector {
        get {
            return Quaternion.FromToRotation(RotationAxis, RotationAxis * 90);
        }
    }

    void Start() {
        isOpen = false;
    }

    void Update() {
        
        if (isOpen && timePassed < OpenTime) {
            timePassed += Time.deltaTime;
            gameObject.transform.Rotate(RotationAxis, Angle / OpenTime * Time.deltaTime);
        }
    }

    public void DoAction(string key) {
        if (key == "" || this.key == key)
        {
            Debug.Log("Sesamé open");
            isOpen = true;
        }
    }
}
