using UnityEngine;
using Assets.Scripts_v02;
using System.Collections;

public class GameManagerFactory : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FourDManager.Construct();
	}
}
