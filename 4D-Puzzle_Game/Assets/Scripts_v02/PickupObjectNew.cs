using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Scripts_v02.FourthDimension;
using Assets.Scripts_v02.Pickupables;
using Assets.Scripts_v02.Interactives;

namespace Assets.Scripts_v02 {
    public class PickupObjectNew : FourthDimensionNew {

        #region Fields & Properties
        public static int PlayerW;
        public static int MaxObjectW = 4;
        public static int MinObjectW = 0;
		public static int MaxPlayerW = 1;

        // Public fields
        public List<string> Inventory = new List<string>(1);
        public float Distance;

        // Private fields
        private GameObject mainCamera;
        private GameObject carriedObject;
        private float carryingDistance;

        private float defaultCarryingDistance {
            get { return Distance / 2; }
        }

        private bool IsCarrying {
            get { return carriedObject != null; }
        }
        #endregion

        protected override void Start() {
            base.Start();
            carryingDistance = defaultCarryingDistance;
            mainCamera = GameObject.FindWithTag("MainCamera");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected override void Update() {
            base.Update();
            if (IsCarrying) {
                CheckDrop();
                Carry();
            } else {
                CheckPickUp();
            }

            var shiftUp = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow);
            if (shiftUp && PlayerW < MaxPlayerW) {
                PushW(1);
                PlayerW = W;
                if (IsCarrying) {
                    var cObj_FD = carriedObject.GetComponent<IFourthDimension>();
                    if (cObj_FD.CanGoWUp()) cObj_FD.PushW(1);
                    else Drop();
                }
            }

            var shiftDown = Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.DownArrow);
            if (shiftDown && PlayerW > 0) {
                PushW(-1);
                PlayerW = W;
                if (IsCarrying) {
                    var cObj_FD = carriedObject.GetComponent<IFourthDimension>();
                    if (cObj_FD.CanGoWDown()) cObj_FD.PushW(-1);
                    else Drop();
                }
            }
        }

        private void Carry() {
            if (IsCarrying) {
                CheckDistance();
                
                Vector3 diff = mainCamera.transform.position + mainCamera.transform.forward * carryingDistance - carriedObject.transform.position;
                Vector3 gO_velocity = diff * 10;

                var pickupable = carriedObject.GetComponent<IPickupable>();
                var itemName = pickupable.Carry(gO_velocity);
                if (!Inventory.Contains(itemName)) {
                    Inventory.Add(itemName);
                }
            }
        }

        private void CheckPickUp() {
            if (Input.GetKeyDown(KeyCode.E)) {
                int x = Screen.width / 2;
                int y = Screen.height / 2;

                Ray pickupRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
                RaycastHit hit;
                var collisionMask = 1 << 8 + PlayerW;
                if (Physics.Raycast(pickupRay, out hit, Distance, collisionMask)) {
                    IPickupable p = hit.collider.GetComponent<IPickupable>();
                    if (p != null) {
                        carriedObject = p.PickUp();
                    }

                    IUsable i = hit.collider.GetComponent<IUsable>();
                    if (i != null) {
                        var correctKey = String.Empty;
                        foreach (string s in Inventory)
                            correctKey = i.Interact(s) ? s : String.Empty;

                        if (!String.IsNullOrEmpty(correctKey))
                            Inventory.Remove(correctKey);
                    }

                    InteractiveDoor id = hit.collider.GetComponent<InteractiveDoor>();
                    if (id != null) {
                        foreach (string k in Inventory) {
                            Debug.Log("Try key: " + k);
                            id.DoAction(k);
                        }
                    }
                }
            }
        }

        private void CheckDrop() {
            if (Input.GetKeyDown(KeyCode.E)) {
                Drop();
            }
        }
        private void Drop() {
            carriedObject.GetComponent<IPickupable>().Drop();
            carriedObject = null;
            carryingDistance = defaultCarryingDistance;
        }

        private void CheckDistance() {
            var newCarryingDist = carryingDistance + Input.GetAxis("Mouse ScrollWheel");
            carryingDistance = Mathf.Clamp(newCarryingDist, 1, Distance);
        }
    }
}
