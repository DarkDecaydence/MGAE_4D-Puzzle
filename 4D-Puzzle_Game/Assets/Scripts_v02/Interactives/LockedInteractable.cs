using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class LockedInteractable : ILockable, IUsable {

        public List<string> PossibleKeys = new List<string>();
        public List<IUsable> Targets = new List<IUsable>();
        public bool IsLocked;
        public int RequiredActivates;

        private int currentActivates;

        public bool Interact(string parameter) {
            TryUnlock(parameter);
            if (!IsLocked) {
                foreach (IUsable u in Targets)
                    u.Interact(parameter);
                return true;
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
