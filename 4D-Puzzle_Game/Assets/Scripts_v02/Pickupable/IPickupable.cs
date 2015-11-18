using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts_v02.Pickupable {
    interface IPickupable {
        bool CanGoWUp();
        bool CanGoWDown();
    }
}
