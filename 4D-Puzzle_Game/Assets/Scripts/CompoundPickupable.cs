using UnityEngine;
using System.Collections;

public class CompoundPickupable : Pickupable {

 
	// Use this for initialization
    public CompoundPickupable[] Family;

    public bool CanGoWUp {
         get  { 
            int max = 0;
            foreach(CompoundPickupable c in Family){
                if(max < c.W){
                    max = c.W;
                } 
            }
            return max < PickupObject.MaxObjectW;
        }

        private set { }
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
        private set { }
    }

    protected override void Start() {
        IsCompound = true;
        base.Start();
    }
}
