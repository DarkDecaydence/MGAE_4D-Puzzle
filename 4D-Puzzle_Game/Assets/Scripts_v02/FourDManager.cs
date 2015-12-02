using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts_v02 {
    public class FourDManager {

        #region Fields & Properties
        /* Singleton static fields */
        private static FourDManager instance;
        public static FourDManager Instance {
            get {
                if (instance == null) Construct();
                return instance;
            }
            private set { instance = value; }
        }

        /* Instance fields */
        public int MinObjectW;
        public int MaxObjectW;
        public int PlayerOffset;

        public int MaxPlayerW {
            get { return MaxObjectW - PlayerOffset; }
        }

        public Color GetDiffColor(int diffW) {
            switch (diffW) {
                case 0: return new Color(1, 1, 1, 1);
                case 1: return new Color(1, 1, 1, 0.0f);
                default: return new Color(1, 1, 1, 0.0f);
            }
        }

        // Currently unused
        private readonly List<LevelLimitSettings> levelLimits = new List<LevelLimitSettings>();
        #endregion

        #region Constructor
        private FourDManager() : this(0, 2, 0) { }

        private FourDManager(int minW, int maxW, int playerOffset) {
            MinObjectW = minW;
            MaxObjectW = maxW;
            PlayerOffset = playerOffset;
        }

        public static void Construct() {
            Instance = new FourDManager();
        }

        public static void Construct(int minW, int maxW, int playerOffset) {
            Instance = new FourDManager(minW, maxW, playerOffset);
        }
        #endregion

        public void SetMaterialRenderMode(Material m, float newMode) {
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

        struct LevelLimitSettings {
            int maxW;
            int minW;
            int offset;

            public LevelLimitSettings(int minW, int maxW, int offset) {
                this.minW = minW;
                this.maxW = maxW;
                this.offset = offset;
            }
        }
    }
}
