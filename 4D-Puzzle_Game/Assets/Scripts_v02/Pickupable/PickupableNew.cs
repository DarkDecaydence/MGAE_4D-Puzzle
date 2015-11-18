using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Scripts_v02.FourthDimension;

namespace Assets.Scripts_v02.Pickupable {
    public class PickupableNew : MonoBehaviour, IPickupable, IFourthDimension {

        public int W;
        public bool IsLocked;

        public bool CanGoWDown() {
            throw new NotImplementedException();
        }

        public bool CanGoWUp() {
            throw new NotImplementedException();
        }

        public void SetW(int newW) {
            throw new NotImplementedException();
        }
    }
}
