using UnityEngine;
using System.Collections;

public class CompoundPickupable : Pickupable {

 
	// Use this for initialization
    public CompoundPickupable[] Family;

    protected override void Start() {
        IsCompound = true;
        base.Start();
    }
}
