using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts_v02.FourthDimension;

namespace Assets.Scripts_v02.Pickupables {
    public class HierarchicalPickupable : HierarchicalFourthDimension, IPickupable {

        public bool IsLocked;
        private Rigidbody[] gO_rigidbodies;

        protected override void Start() {
            gO_rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            base.Start();
        }

        public string Carry(Vector3 velocity) {
            foreach (Rigidbody body in gO_rigidbodies) { 
                body.velocity = velocity;
            }

            return String.Empty;
        }

        public void Drop() {
            foreach (Rigidbody body in gO_rigidbodies) {
                body.useGravity = true;
                body.drag = 1;
            }
        }

        public GameObject PickUp() {
            if (!IsLocked) {
                var parentPickupables = gameObject.GetComponentsInParent<IPickupable>();
                if (parentPickupables.Length > 1) {
                    return parentPickupables.Last().PickUp();
                } else {
                    foreach (Rigidbody body in gO_rigidbodies) {
                        body.useGravity = false;
                        body.drag = 0;
                    }
                    return gameObject;
                }
            } else {
                Debug.Log("Wut?");
                return null;
            }
        }
    }
}
