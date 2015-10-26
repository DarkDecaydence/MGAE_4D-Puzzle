using UnityEngine;
using System.Collections;

public class FourthDimension : MonoBehaviour {

    private int w;
    public int W;
    public Material Red;
    public Material Yellow;
    public Material Green;
    public Material Blue;
    public Material Violet;
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
        
        if (diffW == 0)
        {
            obj.GetComponent<Renderer>().material = Red;
        }
        else if (diffW == 1) {
            obj.GetComponent<Renderer>().material = Yellow;
        }
        else if (diffW == 2)
        {
            obj.GetComponent<Renderer>().material = Green;
        }
        else if (diffW == 3)
        {
            obj.GetComponent<Renderer>().material = Blue;
        }
        else if (diffW == 4)
        {
            obj.GetComponent<Renderer>().material = Violet;
        }
    }
}
