using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class LockedInteractable : Interactable, ILockable {
        
        public bool IsLocked;
        public List<string> RequiredKeys;

        private Dictionary<string, bool> RequiredActivates;

        void Start() {  
        }

        public override bool Interact(string parameter) {
            if (IsLocked) TryUnlock(parameter);

            if (!IsLocked) {
                return base.Interact(parameter);
            } else return false;
        }

        public void TryUnlock(string parameter) {
            bool isUnlocked = false;
            if (RequiredActivates.TryGetValue(parameter, out isUnlocked)) {
                isUnlocked = true;
            }
            if (PossibleKeys.Any(s => s.Equals(parameter))) {
                if (RequiredActivates.All(kvp => kvp.Value))
                    IsLocked = !IsLocked;
            }
        }
    }
}
