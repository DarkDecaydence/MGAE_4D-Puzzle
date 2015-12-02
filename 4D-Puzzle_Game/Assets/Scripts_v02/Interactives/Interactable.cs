using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class Interactable : MonoBehaviour, IUsable {
        public List<GameObject> Targets = new List<GameObject>();
        public List<string> PossibleKeys = new List<string>();

        public virtual bool Interact(string parameter) {
            if (PossibleKeys.Any(s => s.Equals(parameter))) {
                return Targets.Any(gObj => gObj.GetComponent<IUsable>().Interact(parameter));
            } else
                return false;
            }
        }
}
