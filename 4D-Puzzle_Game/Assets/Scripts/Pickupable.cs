using UnityEngine;
using System.Collections;

public class Pickupable : FourthDimension {

	// Use this for initialization
    void Start()
    {
        ChangeColor();
        obj.layer = 8 + W;
    }
}
