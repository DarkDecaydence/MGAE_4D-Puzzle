﻿using UnityEngine;
using System.Collections;

public class Pickupable : FourthDimension {

    public bool IsCompound;
    
    
	// Use this for initialization
    void Start()
    {
        IsCompound = false;
        ChangeColor();
        obj.layer = 8 + W;
    }
}
