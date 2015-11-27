using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts_v02.FourthDimension.RenderCorrecters;

namespace Assets.Scripts_v02.FourthDimension {
    public class HierarchicalFourthDimension : MonoBehaviour, IFourthDimension {
        #region Fields & Properties
        // Public fields
        public int W;

        // Private fields
        private readonly List<Renderer> allRenderers = new List<Renderer>();

        private float t_tween;
        private const float tween_speed = 5.0f;

        private readonly List<Color> originColorQueue = new List<Color>();
        private readonly List<Color> targetColorQueue = new List<Color>();

        // Properties
        private int diffWOld { get; set; }

        private int diffW {
            get { return Math.Abs(W - PickupObjectNew.PlayerW); }
        }

        private Color actualColor {
            get {
                switch (diffW) {
                    case 0: return new Color(1, 1, 1, 1);
                    case 1: return new Color(1, 1, 1, 0.33f);
                    default: return new Color(1, 1, 1, 0.0f);
                }
            }
        }
        
        #endregion

        protected virtual void Start() {
            var potentialRenderers = new List<Renderer>(gameObject.GetComponentsInChildren<Renderer>());
            foreach (Renderer r in potentialRenderers)
                allRenderers.Add(r);

            PushW(0);
        }

        protected virtual void Update() {
            if (diffWOld != diffW)
                ChangeColor();

            TweenChangeColor();
            diffWOld = diffW;
        }

        public void SetW(int newW) {
            var moveAllowed = newW - W < 0 ? CanGoWDown() : CanGoWUp();
            var allGameObjects = gameObject.GetComponentsInChildren<Transform>();

            if (moveAllowed) {
                W = newW;
                foreach (Transform t in allGameObjects) {
                    // Move 4D children
                    var childFD = t.gameObject.GetComponent<IFourthDimension>();
                    if (childFD != null && !t.Equals(transform)) {
                        childFD.SetW(newW);
                    } else {
                        t.gameObject.layer = 8 + W;
                    }
                }
            } else {
                gameObject.layer = 8 + W;
            }
        }

        public void PushW(int wDiff) {
            var moveAllowed = wDiff < 0 ? CanGoWDown() : CanGoWUp();
            var allGameObjects = gameObject.GetComponentsInChildren<Transform>();

            if (moveAllowed) {
                W += wDiff;
                foreach (Transform t in allGameObjects) {
                    // Move 4D children
                    var childFD = t.gameObject.GetComponent<IFourthDimension>();
                    if (childFD != null && !t.Equals(transform)) {
                        childFD.PushW(wDiff);
                    } else {
                        t.gameObject.layer = 8 + W;
                    }
                }
            } else {
                gameObject.layer = 8 + W;
            }
        }

        private void ChangeColor() {
            var currentColor =
                diffWOld == 0 ? new Color(1, 1, 1, 1) :
                diffWOld == 1 ? new Color(1, 1, 1, 0.15f) :
                                new Color(1, 1, 1, 0f);

            originColorQueue.Add(currentColor);
            targetColorQueue.Add(actualColor);
        }

        private void TweenChangeColor() {
            if (targetColorQueue.Count > 0) {
                // Setup Tween
                var originColor = originColorQueue[0];
                var targetColor = targetColorQueue[0];

                t_tween = Mathf.Clamp01(t_tween + Time.deltaTime * tween_speed);
                var tweenedColor = Color.Lerp(originColor, targetColor, t_tween);

                // Iterate and Tween Colors
                for (int i = 0; i < allRenderers.Count; i++) {
                    var curRenderer = allRenderers[i];
                    if (curRenderer == null) {
                        allRenderers.Remove(curRenderer);
                        continue;
                    }

                    // Yield if child contains new 4D Script or BillboardRenderer
                    var skipCriteria =
                        (curRenderer.gameObject.GetComponent<IFourthDimension>() != null && !curRenderer.gameObject.Equals(gameObject)) ||
                        curRenderer is BillboardRenderer;
                    if (skipCriteria) { continue; }

                    IRenderCorrecter correcter = null;
                    IRenderCorrecter[] correcters = curRenderer.gameObject.GetComponentsInParent<IRenderCorrecter>();
                    switch (correcters.Length) {
                        case 0:
                            correcter = null; break;
                        case 1:
                            correcter = correcters[0]; break;
                        default:
                            correcter = correcters[0];
                            Debug.LogWarning("Multiple RenderCorrectors are in use.");
                            break;
                    }
                    
                    foreach (Material m in curRenderer.materials) {
                        var defaultRenderMode = correcter != null ? correcter.GetDefaultMaterialMode(m) : 0f;

                        var isDefaultRender = Mathf.Approximately(m.GetFloat("_Mode"), defaultRenderMode);
                        if (diffW > 0 && isDefaultRender)
                            setMaterialRenderMode(m, 2f);

                        m.color = tweenedColor;

                        if (diffW == 0 && !isDefaultRender && Mathf.Approximately(t_tween, 1f)) {
                            setMaterialRenderMode(m, defaultRenderMode);
                        }
                    }
                }

                if (Mathf.Approximately(t_tween, 1f)) {
                    originColorQueue.RemoveAt(0);
                    targetColorQueue.RemoveAt(0);
                    t_tween = 0f;
                }
            }
        }

        public bool CanGoWUp() {
            foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
                if (t.Equals(gameObject.transform)) continue;

                if (t.GetComponent<IFourthDimension>() != null &&
                    !t.GetComponent<IFourthDimension>().CanGoWUp()) {
                    return false;
                }
            }

            if (W >= PickupObjectNew.MaxObjectW) { return false; }
            
            return true;
        }

        public bool CanGoWDown() {
            foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
                if (t.Equals(gameObject.transform)) continue;

                if (t.GetComponent<IFourthDimension>() != null &&
                    !t.GetComponent<IFourthDimension>().CanGoWDown())
                    return false;
            }

            if (W <= PickupObjectNew.MinObjectW) { return false; }

            return true;
        }

        private void setMaterialRenderMode(Material m, float newMode) {
            m.SetFloat("_Mode", newMode);
            var integerRenderMode = Mathf.RoundToInt(newMode);

            switch (integerRenderMode) {
                case 0: 
                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    m.SetInt("_ZWrite", 1);
                    m.DisableKeyword("_ALPHATEST_ON");
                    m.DisableKeyword("_ALPHABLEND_ON");
                    m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    m.renderQueue = -1;
                    break;
                case 1:
                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    m.SetInt("_ZWrite", 1);
                    m.EnableKeyword("_ALPHATEST_ON");
                    m.DisableKeyword("_ALPHABLEND_ON");
                    m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    m.renderQueue = 2450;
                    break;
                case 2:
                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    m.SetInt("_ZWrite", 0);
                    m.DisableKeyword("_ALPHATEST_ON");
                    m.EnableKeyword("_ALPHABLEND_ON");
                    m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    m.renderQueue = 3000;
                    break;
                case 3:
                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    m.SetInt("_ZWrite", 0);
                    m.DisableKeyword("_ALPHATEST_ON");
                    m.DisableKeyword("_ALPHABLEND_ON");
                    m.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    m.renderQueue = 3000;
                    break;
            }
        }
    }
}
