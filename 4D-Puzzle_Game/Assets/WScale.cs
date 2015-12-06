using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_v02;

public class WScale : MonoBehaviour {

    public List<Image> ImageRenderers;

    void Start() {

    }
    // Use this for initialization
    void Update() {
        foreach (Image i in ImageRenderers) {
            i.color *= new Color(1, 1, 1, 0);
        }

        ImageRenderers.ElementAt(PickupObjectNew.PlayerW).color = new Color(1, 1, 1, 1);
	}
}
