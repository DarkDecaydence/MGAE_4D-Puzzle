using UnityEngine;
using System.Collections;

public class FourthDimension : MonoBehaviour {

    private int w;
    public int W;
    private int diffW;
    private int diffWold;
    public GameObject obj;

	// Use this for initialization
	void Start () {
        ChangeColor();
        obj.layer = 8 + W;
    }
	
	// Update is called once per frame
	void Update () {
        diffW = (int)Mathf.Abs(W - PController.playerW); 
        if (diffW != diffWold) {
            ChangeColor();
        }
        diffWold = diffW;
	}

    private void ChangeColor()
    {
        var alpha = 0.9f - diffW * 0.2f;
        
        if (diffW == 0)
        {
            obj.GetComponent<Renderer>().material.color = new Color(1, 0, 0, alpha);
        }
        else if (diffW == 1) {
            obj.GetComponent<Renderer>().material.color = new Color(1, 0.92f, 0.016f, alpha);
        }
        else if (diffW == 2)
        {
            obj.GetComponent<Renderer>().material.color = new Color(0, 1, 0, alpha);
        }
        else if (diffW == 3)
        {
            obj.GetComponent<Renderer>().material.color = new Color(0, 0, 1, alpha);
        }
        else if (diffW == 4)
        {
            obj.GetComponent<Renderer>().material.color = new Color(0.93f, 0.51f, 0.93f, alpha);
        }
    }
}
