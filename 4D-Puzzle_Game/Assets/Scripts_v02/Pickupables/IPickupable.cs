using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts_v02.Pickupables {
    interface IPickupable {
        GameObject PickUp();
        string Carry(Vector3 velocity);
        void Drop();
    }
}
