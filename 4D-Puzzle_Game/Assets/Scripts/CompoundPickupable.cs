using UnityEngine;
using System.Collections;

public class CompoundPickupable : Pickupable {
    
        
	public CompoundPickupable[] Family;


    public bool CanGoWUp {
         get  { 
            int max = 0;
            foreach(CompoundPickupable c in Family){
                if(max < c.W) {
                    max = c.W;
                } 
            }
            return max < PickupObject.MaxObjectW;
        }
    }

    public bool CanGoWDown
    {
        get
        {
            int min = PickupObject.MaxObjectW;
            foreach (CompoundPickupable c in Family)
            {
                if (min > c.W)
                {
                    min = c.W;
                }
            }
            return min > PickupObject.MinObjectW;
        }
    }

    protected override void Start() {
        IsCompound = true;
        base.Start();
    }
}
