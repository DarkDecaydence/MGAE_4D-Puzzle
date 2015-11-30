using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class LockedInteractable : Interactable, ILockable {
        
        public bool IsLocked;
        public int RequiredActivates;

        private int currentActivates;

        public override bool Interact(string parameter) {
            if (IsLocked) TryUnlock(parameter);

            if (!IsLocked) {
                return base.Interact(parameter);
            } else return false;
        }

        public void TryUnlock(string parameter) {
            if (PossibleKeys.Any(s => s.Equals(parameter))) {
                currentActivates++;
                if (currentActivates >= RequiredActivates)
                    IsLocked = !IsLocked;
            }
        }
    }
}
