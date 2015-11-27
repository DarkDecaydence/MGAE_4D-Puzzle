using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts_v02.Interactives {
    public class LockedInteractable : ILockable, IUsable {

        public List<IUsable> Targets = new List<IUsable>();
        public bool IsLocked;
        public int RequiredActivates;

        private int currentActivates;

        public bool Interact(string parameter) {
            TryUnlock();
            if (!IsLocked) {
                foreach (IUsable u in Targets)
                    u.Interact(String.Empty);
                return true;
            } else return false;
        }

        public void TryUnlock() {
            currentActivates++;
            if (currentActivates >= RequiredActivates)
                IsLocked = !IsLocked;
        }
    }
}
