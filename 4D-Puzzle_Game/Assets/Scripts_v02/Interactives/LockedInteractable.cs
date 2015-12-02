using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class LockedInteractable : Interactable, ILockable {
        
        public bool IsLocked;
        public List<string> RequiredKeys;

        private Dictionary<string, bool> requiredActivates = new Dictionary<string, bool>();

        void Start() {
            foreach (string s in RequiredKeys) {
                requiredActivates.Add(s, false);
            }
        }

        public override bool Interact(string parameter) {
            if (IsLocked) TryUnlock(parameter);

            if (!IsLocked) {
                return base.Interact(parameter);
            } else return false;
        }

        public void TryUnlock(string parameter) {
            bool isUnlocked = false;
            if (requiredActivates.TryGetValue(parameter, out isUnlocked)) {
                if (!isUnlocked) {
                    requiredActivates.Remove(parameter);
                    requiredActivates.Add(parameter, true);
                }
            }

            if (PossibleKeys.Any(s => s.Equals(parameter))) {
                if (requiredActivates.All(kvp => kvp.Value))
                    IsLocked = !IsLocked;
            }
        }
    }
}
