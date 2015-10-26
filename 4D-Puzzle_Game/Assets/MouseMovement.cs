using UnityEngine;
using System.Collections;

public class MouseMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        this.transform.Translate(new Vector3(mouseX, 0, mouseY));
	}
}
