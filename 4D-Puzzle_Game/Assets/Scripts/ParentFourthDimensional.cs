using UnityEngine;
using System.Collections.Generic;

public class ParentFourthDimensional : MonoBehaviour {

    public int W;

#if UseColors
    /*
    protected static Color[] w_Colors = new Color[] {
        new Color(1, 0, 0, 1f),
        new Color(1, 0.92f, 0.016f, 0.33f),
        new Color(0, 1, 0, 0.0f),
        new Color(0, 0, 1, 0.0f),
        new Color(0.93f, 0.51f, 0.93f, 0.0f)
    };
    */
#else
    protected static Color[] w_Colors = new Color[] {
        new Color(1, 1, 1, 1),
        new Color(1, 1, 1, 0.33f),
        new Color(1, 1, 1, 0.0f),
        new Color(1, 1, 1, 0.0f),
        new Color(1, 1, 1, 0.0f)
    };
#endif

    private int diffW;
    private int diffWold;
    protected Color visibleColor;
    protected float t_tween;
    protected const float tween_speed = 5.0f;
    protected readonly List<Color> targetColorQueue = new List<Color>();

    private bool isOpaque;
    
    void Start () {
        foreach (Transform child in gameObject.transform) {
            var childRenderer = child.gameObject.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                if (childRenderer.materials.Length > 1) {
                    var debugMsg = string.Format("{0} -> {1} children... Child of {2}\n",
                        childRenderer.gameObject.name, childRenderer.materials.Length, gameObject.transform.parent.name);
                    var matList = string.Empty;
                    foreach (Material mat in childRenderer.materials) {
                        matList += mat.name + "\n";
                    }
                    debugMsg = debugMsg + matList;
                    Debug.Log(debugMsg);
                }
                foreach (Material m in childRenderer.materials) { 
                    childRenderer.material.color = w_Colors[W];
                }
            }
        }
        diffWold = -1;
        SetW(W);
    }

    protected virtual void Update()
    {
        diffW = (int)Mathf.Abs(W - PickupObject.playerW);
        if (diffW != diffWold)
            ChangeColor();

        diffWold = diffW;
        TweenChangeColor();
    }

    protected void ChangeColor()
    {
        targetColorQueue.Add(w_Colors[diffW]);
    }

    public virtual void SetW(int w)
    {
        W = w;
        foreach (Transform child in transform) {
            child.gameObject.layer = 8 + W;
        }
    }

    private void TweenChangeColor()
    {
        Color tweenedColor = visibleColor;
        if (targetColorQueue.Count > 0)
        {
            t_tween = Mathf.Clamp01(t_tween + Time.deltaTime * tween_speed);

            var nextColor = targetColorQueue[0];

            tweenedColor = Color.Lerp(visibleColor, nextColor, t_tween);
            
            foreach (Transform child in transform) {
                var childRenderer = child.gameObject.GetComponent<Renderer>();
                if (childRenderer != null) {
                    childRenderer.material.color = tweenedColor;

                    if (diffW > 0 && isOpaque) {
                        foreach (Material m in childRenderer.materials) {
                            setMaterialToFade(m);
                        }
                    }
                }
            }

            if (diffW > 0 && isOpaque) {
                isOpaque = false;
            }
        }

        if (Mathf.Approximately(t_tween, 1f))
        {
            foreach (Transform child in transform) {
                var childRenderer = child.gameObject.GetComponent<Renderer>();
                if (childRenderer != null) {
                    if (diffW == 0 && !isOpaque) {
                        foreach (Material m in childRenderer.materials) { setMaterialToOpaque(m); }
                        isOpaque = true;
                    }
                }
            }

            visibleColor = targetColorQueue[0];
            targetColorQueue.RemoveAt(0);
            t_tween = 0f;
        }
    }
    
    private void setMaterialToFade(Material m) {
        m.SetFloat("_Mode", 2);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.EnableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = 3000;
    }

    private void setMaterialToOpaque(Material m) {
        m.SetFloat("_Mode", 0);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        m.SetInt("_ZWrite", 1);
        m.DisableKeyword("_ALPHATEST_ON");
        m.DisableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = m.shader.renderQueue;
    }
}

