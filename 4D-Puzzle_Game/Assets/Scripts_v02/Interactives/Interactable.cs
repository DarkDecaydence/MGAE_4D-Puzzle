using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts_v02.Interactives {
    public class Interactable : MonoBehaviour, IUsable {
        public List<IUsable> Targets = new List<IUsable>();
        public List<string> PossibleKeys = new List<string>();
        public GameObject Target;

        public bool Interact(string parameter) {

            Target.GetComponent<IUsable>().Interact("");/*
            if (PossibleKeys.Any(s => s.Equals(parameter))) {
                foreach (IUsable u in targets)
                    u.Interact(parameter);
                */
                return true;
            

          //       return false;
        }
    }
}
