using UnityEngine;
using System.Collections.Generic;

public class FourthDimensionLock : FourthDimension {

    public bool IsLocked;
    
    private Color lockColor = new Color(0, 0, 0);

	// Use this for initialization
	protected sealed override void Start () {
        IsLocked = true;
        if (obj.GetComponent<Renderer>())
            obj.GetComponent<Renderer>().material.color = lockColor;
        obj.layer = 0;
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
        obj.layer = 8 + W;
    }

    public void CloseLock() {
        IsLocked = true;
        if (obj.GetComponent<Renderer>())
            obj.GetComponent<Renderer>().material.color = lockColor;
        obj.layer = 0;
    }
}
