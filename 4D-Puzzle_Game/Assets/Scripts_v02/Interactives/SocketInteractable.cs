using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts_v02.Pickupables;

namespace Assets.Scripts_v02.Interactives {
    public class SocketInteractable : Interactable {

        void OnTriggerEnter(Collider other) {
            var potentialKey = other.gameObject.name;

            if (PossibleKeys.Any(s => s == potentialKey)) {
                DisableKey(other.gameObject);
                var werks = false;
                werks = base.Interact(potentialKey) ? true : base.Interact(String.Empty);
                Debug.Log(werks);
            }
        }

        void DisableKey(GameObject gObj) {
            gObj.GetComponent<IPickupable>().Lock(transform);
        }
    }
}
