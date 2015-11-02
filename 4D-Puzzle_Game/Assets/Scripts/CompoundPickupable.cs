using UnityEngine;
using System.Collections;

public class CompoundPickupable : Pickupable {

 
	// Use this for initialization
    public CompoundPickupable[] Family;

    void Start() {
        IsCompound = true;
    }
}
