using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
	GameObject carriedObject;
	public float distance;
	public float smooth;
    public FourthDimension Fourth;
    public static int playerW;
    public float speed;
    private float floatW;
    public GameObject obj;

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
        if (!o.GetComponent<Pickupable>().IsCompound)
        {

            Vector3 diff = mainCamera.transform.position + mainCamera.transform.forward * distance - o.transform.position;
            o.GetComponent<Rigidbody>().AddForce(diff*10);
            
            //o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
            //o.transform.rotation = Quaternion.identity;
        }
        else if (o.GetComponent<Pickupable>().IsCompound) {
            Vector3 diff = mainCamera.transform.position + mainCamera.transform.forward * distance - o.transform.position;
            foreach (CompoundPickupable c in o.GetComponent<CompoundPickupable>().Family)
            {
                c.GetComponent<Rigidbody>().AddForce(diff*10);
                c.GetComponent<Rigidbody>().drag = 2.5f;
               // c.transform.rotation = Quaternion.identity;
        
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
				if(p != null) {
					FourthDimension pf = p.gameObject.GetComponent<FourthDimension>();
                  //  if (pf.W == Fourth.W)
                    {

                        carrying = true;
                        carriedObject = p.gameObject;
                        if (!p.IsCompound)
                        {
                            //					p.gameObject.rigidbody.isKinematic = true;
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
                carriedObject = null;
            }
        }
        else
        {
            carrying = false;
            //	carriedObject.gameObject.rigidbody.isKinematic = false;
            carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject = null;
        }
	}
    
    void SetW(int w)
    {
        FourthDimension otherScript;
        otherScript = GetComponent<FourthDimension>();
        otherScript.W = w;
        playerW = w;
        obj.layer = 8 + playerW;
        if (carrying) {
            carriedObject.gameObject.layer = obj.layer;
            carriedObject.GetComponent<Pickupable>().SetW(playerW);
        }
    }

}
