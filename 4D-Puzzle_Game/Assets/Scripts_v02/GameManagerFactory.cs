using UnityEngine;

namespace Assets.Scripts_v02 { 
    public class GameManagerFactory : MonoBehaviour {
	    void Start () {
            FourDManager.Construct();
	    }
    }
}
