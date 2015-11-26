using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts_v02.FourthDimension.RenderCorrecters {
    public class RenderCorrecter : MonoBehaviour, IRenderCorrecter {
        private readonly Dictionary<Material, float> materialModes = new Dictionary<Material, float>();

        void Start() {
            foreach (Material m in gameObject.GetComponent<Renderer>().materials)
                materialModes.Add(m, m.GetFloat("_Mode"));
        }

        public float GetDefaultMaterialMode(Material m) {
            float mode = -1f;
            if (materialModes.TryGetValue(m, out mode)) {
                return mode;
            }
            throw new ArgumentException("Material '" + m.name + "' does not exist in dictionary");
        }
    }
}
