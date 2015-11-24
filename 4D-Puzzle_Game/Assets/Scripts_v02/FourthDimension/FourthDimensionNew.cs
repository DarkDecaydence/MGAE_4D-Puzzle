using UnityEngine;
using System.Collections.Generic;
using System;

namespace Assets.Scripts_v02.FourthDimension {
    public class FourthDimensionNew : MonoBehaviour, IFourthDimension {
        #region Fields & Properties
        // Public fields
        public int W;

        // Private fields
        private Renderer gObjRenderer;
        private float defaultRenderMode;
        private float currentRenderMode;

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
            if (gameObject.GetComponent<Renderer>() != null) { 
                gObjRenderer = gameObject.GetComponent<Renderer>();
                currentRenderMode = defaultRenderMode = gObjRenderer.material.GetFloat("_Mode");
            }

            SetW(W);
        }

        protected virtual void Update() {
            if (diffWOld != diffW)
                ChangeColor();

            TweenChangeColor();
            diffWOld = diffW;
        }

        public void SetW(int newW) {
            W = newW;
            gameObject.layer = 8 + W;
        }

        private void ChangeColor() {
            var currentColor =
                diffWOld == 0 ? new Color(1, 1, 1, 1) :
                diffWOld == 1 ? new Color(1, 1, 1, 0.33f) :
                                new Color(1, 1, 1, 0f);

            originColorQueue.Add(currentColor);
            targetColorQueue.Add(actualColor);
        }

        private void TweenChangeColor() {
            if (gObjRenderer != null && targetColorQueue.Count > 0) {

                // Will always run if defaultRenderMode == 2
                var isDefaultRender = Mathf.Approximately(defaultRenderMode, currentRenderMode);
                if (diffW > 0 && isDefaultRender) {
                    currentRenderMode = 2f; // Render mode #2 is Fade.
                    setMaterialRenderMode(gObjRenderer.material);
                }

                var originColor = originColorQueue[0];
                var targetColor = targetColorQueue[0];

                t_tween = Mathf.Clamp01(t_tween + Time.deltaTime * tween_speed);
                var tweenedColor = Color.Lerp(originColor, targetColor, t_tween);

                gObjRenderer.material.color = tweenedColor;

                if (Mathf.Approximately(t_tween, 1f)) {
                    if (diffW == 0 && !isDefaultRender) {
                        currentRenderMode = defaultRenderMode;
                        setMaterialRenderMode(gObjRenderer.material);
                    }

                    originColorQueue.RemoveAt(0);
                    targetColorQueue.RemoveAt(0);
                    t_tween = 0f;
                }
            }
        }

        public bool CanGoWUp() { return W > PickupObject.MinObjectW; }
        public bool CanGoWDown() { return W < PickupObject.MaxObjectW; }

        private void setMaterialRenderMode(Material m) {
            m.SetFloat("_Mode", currentRenderMode);
            
            if (currentRenderMode == 2 || currentRenderMode == 0) {
                m.DisableKeyword("_ALPHATEST_ON");
                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            }

            if (currentRenderMode == 0) {
                m.SetInt("_ZWrite", 1);
                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                m.DisableKeyword("_ALPHABLEND_ON");
                m.renderQueue = m.shader.renderQueue;
            } else if (currentRenderMode == 2) {
                m.SetInt("_ZWrite", 0);
                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                m.EnableKeyword("_ALPHABLEND_ON");
                m.renderQueue = 3000;
            }
        }
    }
}
