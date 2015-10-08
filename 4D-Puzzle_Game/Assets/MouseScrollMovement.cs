using UnityEngine;
using System.Collections;
using System;

public class MouseScrollMovement : MonoBehaviour {

    public Vector3 movementVector;
    
    private Renderer thisRenderer;
    public float wCoord = 0;
    public float maxW;

	// Use this for initialization
	void Start () {
        thisRenderer = GetComponent<MeshRenderer>();
        thisRenderer.material.color = new Color(0, 0, 0);
        thisRenderer.material.color = ChangeColor(thisRenderer.material.color, wCoord * 2.5f);
    }
	
	// Update is called once per frame
	void Update () {
        var scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        var oldWCoord = wCoord;

        wCoord += scrollDelta;
        if (wCoord < 0) wCoord = 0;
        if (wCoord > maxW) wCoord = maxW;

        var deltaW = wCoord - oldWCoord;

        thisRenderer.material.color = ChangeColor(thisRenderer.material.color, wCoord / maxW);
        var curMovement = movementVector * deltaW;
        gameObject.transform.position += curMovement;
	}
    
    private Color ChangeColor(Color curColor, float newGreen)
    {
        var newColor = new Color(thisRenderer.material.color.r,
                                 newGreen,
                                 thisRenderer.material.color.b);
        return newColor;
    }
}
