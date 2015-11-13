using UnityEngine;
using System.Collections.Generic;

public class ParentFourthDimensional : MonoBehaviour {

    public int W;
    
    protected static Color[] w_Colors = new Color[] {
        new Color(1, 1, 1, 1),
        new Color(1, 1, 1, 0.5f),
        new Color(1, 1, 1, 0.0f),
        new Color(0, 0, 1, 0.0f),
        new Color(0.93f, 0.51f, 0.93f, 0.0f)
    };
    private int diffW;
    private int diffWold;
    protected Color visibleColor;
    protected float t_tween;
    protected const float tween_speed = 5.0f;
    protected readonly List<Color> targetColorQueue = new List<Color>();
    
    void Start () {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<Renderer>() != null)
            {
                child.gameObject.GetComponent<Renderer>().material.color = w_Colors[W];
                visibleColor = w_Colors[W];

            }
            ChangeColor();
            gameObject.layer = 8 + W;
        }
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

            tweenedColor =
                new Color(
                    Mathf.Lerp(visibleColor.r, nextColor.r, t_tween),
                    Mathf.Lerp(visibleColor.g, nextColor.g, t_tween),
                    Mathf.Lerp(visibleColor.b, nextColor.b, t_tween),
                    Mathf.Lerp(visibleColor.a, nextColor.a, t_tween)
            );
        }
        foreach(Transform child in transform){
            var renderer = child.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {        
                renderer.material.color = tweenedColor;
            }
        }

        if (Mathf.Approximately(t_tween, 1f))
        {
            visibleColor = targetColorQueue[0];
            targetColorQueue.RemoveAt(0);
            t_tween = 0f;
        }
    }
}

