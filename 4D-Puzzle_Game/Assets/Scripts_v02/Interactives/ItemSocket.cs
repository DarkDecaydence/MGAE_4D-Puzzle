using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts_v02.Interactives {
    public class ItemSocket : MonoBehaviour, IUsable {

        public string RequiredKey;

        public void Interact(object[] parameters) {
            if (parameters.Length < 1) 
                throw new ArgumentException("Cannot interact with ItemSocket using empty parameters");

            var key = (string)parameters[0];

            if (RequiredKey == key) {
                
            }
        }
    }
}
