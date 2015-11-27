using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class Interactable : MonoBehaviour, IUsable {

        public List<string> PossibleKeys;
        public List<IUsable> Targets = new List<IUsable>();

        public bool Interact(string parameter) {
            if (PossibleKeys.Any(s => s.Equals(parameter))) {
                foreach (IUsable u in Targets)
                    u.Interact(String.Empty);

                return true;
            }

            return false;
        }
    }
}
