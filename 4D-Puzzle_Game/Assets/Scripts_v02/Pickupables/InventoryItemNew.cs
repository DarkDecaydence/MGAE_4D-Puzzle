using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts_v02.FourthDimension;
using System;

namespace Assets.Scripts_v02.Pickupables {
    public class InventoryItemNew : FourthDimensionNew, IPickupable {

        public string ItemName;

        public string Carry(Vector3 velocity) {
            // Destroy GameObject to remove item from world.
            Destroy(gameObject);
            return ItemName;
        }

        public GameObject PickUp() {
            return gameObject;
        }

        public void Drop() { }

        public void Lock(Transform lockTransform) { }
    }
}
