using UnityEngine;
using System.Collections;

public class KeySocket : MonoBehaviour {
    public string RequiredKey;
    public float TimeDelay;
    public GameObject LockedObject;

    private float collisionTimer;

    void OnTriggerStay(Collider other) {
        var potentialKey = other.gameObject.GetComponent<KeySocketable>();

        if (potentialKey == null) {
            Debug.Log("No Key Attached!");
            return;
        }
        else {
            var keyMatch = RequiredKey.Equals(potentialKey.Key);

            if (keyMatch) {
                collisionTimer += Time.deltaTime;

                if (collisionTimer >= TimeDelay) {
                    var lockedObj = LockedObject.GetComponent<FourthDimensionLock>();
                    if (lockedObj != null && lockedObj.IsLocked) {
                        Debug.Log("Key has been socketed!");
                        lockedObj.OpenLock();
                    }
                }
            }
        }
    }
}
