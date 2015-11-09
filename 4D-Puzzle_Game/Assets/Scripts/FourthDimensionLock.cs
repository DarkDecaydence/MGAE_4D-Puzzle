using UnityEngine;
using System.Collections.Generic;

public class FourthDimensionLock : FourthDimension {

    public bool IsLocked;
    
    private Color lockColor = new Color(0, 0, 0);

	// Use this for initialization
	protected sealed override void Start () {
        var renderer = gameObject.GetComponent<Renderer>();

        if (renderer != null) {

            if (IsLocked) {
                renderer.material.color = lockColor;
                gameObject.layer = 0;
            } else {
                ChangeColor();
                gameObject.layer = 8 + W;
            }
        }
    }
	
	// Update is called once per frame
	protected sealed override void Update () {
        if (!IsLocked) {
            base.Update();
        }
	}

    public void OpenLock() {
        IsLocked = false;
        ChangeColor();
        gameObject.layer = 8 + W;
    }

    public void CloseLock() {
        IsLocked = true;
        Start();
    }
}
