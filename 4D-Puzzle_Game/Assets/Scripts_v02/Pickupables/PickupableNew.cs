using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts_v02.FourthDimension;

namespace Assets.Scripts_v02.Pickupables {
    public class PickupableNew : FourthDimensionNew, IPickupable {
        
        public bool IsLocked;
        private Rigidbody gO_rigidbody;

        protected override void Start() {
            gO_rigidbody = gameObject.GetComponent<Rigidbody>();
            base.Start();
        }

        public GameObject PickUp() {
            if (!IsLocked) {
                var parentPickupables = gameObject.GetComponentsInParent<IPickupable>();
                if (parentPickupables.Length > 1) {
                    return parentPickupables.Last().PickUp();
                } else {
                    gO_rigidbody.useGravity = false;
                    gO_rigidbody.drag = 0;
                    return gameObject;
                }
            } else {
                return null;
            }
        }

        public string Carry(Vector3 velocity) {
            gO_rigidbody.velocity = velocity;
            return String.Empty;
        }

        public void Drop() {
            gO_rigidbody.useGravity = true;
            gO_rigidbody.drag = 1;
        }
    }
}
