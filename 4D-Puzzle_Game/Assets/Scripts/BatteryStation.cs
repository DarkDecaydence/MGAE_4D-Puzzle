using UnityEngine;
using System.Collections;

public class BatteryStation : MonoBehaviour
{
    public string RequiredKey;
    public GameObject LockedObject;
    public ElevatorButton[] Targets;

    void OnTriggerStay(Collider other)
    {
        var potentialKey = other.gameObject.GetComponent<KeySocketable>();

        if (potentialKey == null)
        {
            Debug.Log("No Key Attached!");
            return;
        }
        else
        {
            var keyMatch = RequiredKey.Equals(potentialKey.Key);

            if (keyMatch)
            {
                foreach (ElevatorButton e in Targets)
                {
                    e.IsEnabled = true;
                }
                LockKey(other.gameObject);
            }
        }
    }

    void LockKey(GameObject other) {
        other.transform.rotation = gameObject.transform.rotation;
        other.transform.position = gameObject.transform.position;
        other.GetComponent<Pickupable>().IsLocked = true;
    }
}
