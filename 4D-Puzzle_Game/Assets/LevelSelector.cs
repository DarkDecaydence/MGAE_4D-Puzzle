using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour {	

    void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

	// Update is called once per frame
	void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D[] selectionHits = Physics2D.RaycastAll(Input.mousePosition, new Vector2());
            if (selectionHits.Length > 0) {
                foreach (RaycastHit2D rh in selectionHits) {
                    var goto_Script = rh.collider.GetComponent<GoToLevel>();
                    if (goto_Script != null) {
                        Debug.Log("SJFJGAHJABNUJ");
                        Application.LoadLevel(goto_Script.LevelName);
                    }
                }
            }
        }
	}
}
