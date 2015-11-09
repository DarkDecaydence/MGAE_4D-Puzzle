using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

    GameObject mainCamera;
	bool carrying;
	GameObject carriedObject;

    public float Distance;
	//public float Smooth;
    public float Speed;

    public static int playerW;

    public static int MaxPlayerW = 4;
    public static int MinPlayerW = 0;
    public static int MaxObjectW = 4;
    public static int MinObjectW = 0;

    // Use this for initialization
    void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if(carrying) {
			carry(carriedObject);
			checkDrop();
			//rotateObject();
		} else {
			pickup();
		}

        if (Input.GetKeyDown(KeyCode.UpArrow) && playerW < 4)
        {
            SetW(playerW + 1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && playerW > 0)
        {
            SetW(playerW - 1);
        }

	}

	void rotateObject() {
		carriedObject.transform.Rotate(5,10,15);
	}

	void carry(GameObject o) {
        Vector3 diff = mainCamera.transform.position + mainCamera.transform.forward * Distance - o.transform.position;
        var acceleration = diff.sqrMagnitude * 4;
        // [1 / 0.01f -> 1 / 5]
        var newDrag = 1 / Mathf.Clamp(diff.magnitude, 0.01f, 5f) * 5f;

        if (!o.GetComponent<Pickupable>().IsCompound)
        {
            o.GetComponent<Rigidbody>().AddForce(diff * acceleration);
            o.GetComponent<Rigidbody>().drag = newDrag;

            //o.GetComponent<Rigidbody>().AddForce(diff * 10);

            //o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
            //o.transform.rotation = Quaternion.identity;
        }
        else if (o.GetComponent<Pickupable>().IsCompound) {
            foreach (CompoundPickupable c in o.GetComponent<CompoundPickupable>().Family)
            {
                c.GetComponent<Rigidbody>().AddForce(diff * acceleration);
                c.GetComponent<Rigidbody>().drag = newDrag;

                //c.transform.rotation = Quaternion.identity;
                //c.transform.position = Vector3.Lerp(c.transform.position, c.transform.position + diff, Time.deltaTime * smooth);
            }
        }
	}

	void pickup() {
		if(Input.GetKeyDown (KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
            RaycastHit hit;
            var mask = 1 << 8 + playerW;
			if(Physics.Raycast(ray, out hit, 2.0f, mask)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null && !p.IsLocked) {
					FourthDimension pf = p.gameObject.GetComponent<FourthDimension>();
                  //  if (pf.W == Fourth.W)
                    {

                        carrying = true;
                        carriedObject = p.gameObject;
                        if (!p.IsCompound)
                        {
                            // p.gameObject.rigidbody.isKinematic = true;
                            p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        }
                        else if (p.IsCompound) {
                            foreach (CompoundPickupable cp in ((CompoundPickupable)p).Family) {
                                cp.gameObject.GetComponent<Rigidbody>().useGravity = false;
                            }
                        }
                    }
				}

                Interactive i = hit.collider.GetComponent<Interactive>();
                if (i != null) {
                    i.DoAction();
                }
			}
		}
	}

	void checkDrop() {
		if(Input.GetKeyDown (KeyCode.E)) {
			dropObject();
		}
	}

	void dropObject() {
        if (carriedObject.GetComponent<Pickupable>().IsCompound)
        {
            foreach (CompoundPickupable c in carriedObject.gameObject.GetComponent<CompoundPickupable>().Family)
            {
                carrying = false;
                c.GetComponent<Rigidbody>().useGravity = true;
                c.GetComponent<Rigidbody>().drag = 2.5f;
                carriedObject = null;
            }
        }
        else
        {
            carrying = false;
            //	carriedObject.gameObject.rigidbody.isKinematic = false;
            carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject.gameObject.GetComponent<Rigidbody>().drag = 2.5f;
            carriedObject = null;
        }
	}
    
    void SetW(int w)
    {
        FourthDimension otherScript;
        otherScript = GetComponent<FourthDimension>();
        otherScript.W = w;
        playerW = w;
        gameObject.layer = 8 + playerW;
        if (carrying) {
            carriedObject.gameObject.layer = gameObject.layer;
            carriedObject.GetComponent<Pickupable>().SetW(playerW);
        }
    }

}
