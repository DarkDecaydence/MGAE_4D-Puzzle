using UnityEngine;
using System.Collections.Generic;

public class FourthDimension : MonoBehaviour {
    
    public int W;
    private int diffW;
    private int diffWold;
    public GameObject obj;

    protected static Color[] w_Colors = new Color[] {
        new Color(1, 0, 0, 0.9f),
        new Color(1, 0.92f, 0.02f, 0.7f),
        new Color(0, 1, 0, 0.5f),
        new Color(0, 0, 1, 0.3f),
        new Color(0.93f, 0.51f, 0.93f, 0.1f)
    };

    private Color visibleColor;
    private float t_tween;
    private float tween_speed;
    protected List<Color> targetColorQueue;
    
	// Use this for initialization
	void Start () {
        targetColorQueue = new List<Color>();

        if (obj.GetComponent<Renderer>() != null) {
            obj.GetComponent<Renderer>().material.color = w_Colors[W];
            visibleColor = w_Colors[W];
        }

        ChangeColor();
        obj.layer = 8 + W;
        tween_speed = 5;
    }
	
	// Update is called once per frame
	void Update () {

        diffW = (int)Mathf.Abs(W - PickupObject.playerW); 
        if (diffW != diffWold) {
            ChangeColor();
        }
        diffWold = diffW;
        TweenChangeColorNew();
    }

    public void SetW(int w)
    {
        W = w;
        ChangeColor();
    }

    protected void ChangeColor()
    {
        var renderer = obj.GetComponent<Renderer>();
        
        if (renderer != null) {
            if (diffW != diffWold) {
                targetColorQueue.Add(w_Colors[diffW]);
            }
        }
    }
    
    protected void TweenChangeColorNew() {
        var renderer = obj.GetComponent<Renderer>();

        if (renderer != null) {
            if (targetColorQueue.Count > 0) {
                t_tween = Mathf.Clamp01(t_tween + Time.deltaTime * tween_speed);
                
                var nextColor = targetColorQueue[0];

                var tweenedColor =
                    new Color(
                        Mathf.Lerp(visibleColor.r, nextColor.r, t_tween),
                        Mathf.Lerp(visibleColor.g, nextColor.g, t_tween),
                        Mathf.Lerp(visibleColor.b, nextColor.b, t_tween),
                        Mathf.Lerp(visibleColor.a, nextColor.a, t_tween)
                    );

                renderer.material.color = tweenedColor;
            }

            if (Mathf.Approximately(t_tween, 1f)) {
                targetColorQueue.RemoveAt(0);
                visibleColor = renderer.material.color;
                t_tween = 0f;
            }
        }
    }
}
