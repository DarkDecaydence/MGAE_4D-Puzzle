using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class Interactable : MonoBehaviour, IUsable {

        public List<string> PossibleKeys = new List<string>();
        public List<IUsable> Targets = new List<IUsable>();

        public bool Interact(string parameter) {
            if (PossibleKeys.Any(s => s.Equals(parameter))) {
                foreach (IUsable u in Targets)
                    u.Interact(parameter);

                return true;
            }

            return false;
        }
    }
}
