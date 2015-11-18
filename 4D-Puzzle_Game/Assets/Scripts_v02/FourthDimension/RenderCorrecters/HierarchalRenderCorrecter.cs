using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts_v02.FourthDimension.RenderCorrecters {
    public class HierarchalRenderCorrecter : MonoBehaviour, IRenderCorrecter {
        private readonly Dictionary<Material, float> materialModes = new Dictionary<Material, float>();

        void Start() {
            foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>())
                foreach (Material m in r.materials)
                    if (!materialModes.ContainsKey(m))
                        materialModes.Add(m, m.GetFloat("_Mode"));
        }

        public float GetDefaultMaterialMode(Material m) {
            float mode = -1f;
            if (materialModes.TryGetValue(m, out mode)) {
                return mode;
            }
            throw new ArgumentException("Material '" + m.name + "' does not exist in dictionary. GOBJ: '" + gameObject.name + "'");
        }
    }
}
