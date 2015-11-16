﻿using UnityEngine;
using System.Collections.Generic;

public class PickupObject : MonoBehaviour {

    public List<string> Inventory = new List<string>(1);
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

        var shiftUp = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow);
        if (shiftUp && playerW < 4)
        {
            SetW(playerW + 1);
        }

        var shiftDown = Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.DownArrow);
        if (shiftDown && playerW > 0)
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
			if(Physics.Raycast(ray, out hit, Distance, mask)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null && !p.IsLocked) {
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

                InventoryItem it = hit.collider.GetComponent<InventoryItem>();
                if (it != null) {
                    Object.Destroy(hit.collider.gameObject);
                    Inventory.Add(it.ItemName);
                }

                InteractiveDoor id = hit.collider.GetComponent<InteractiveDoor>();
                if (id != null) {
                    foreach (string k in Inventory) {
                        Debug.Log("Try key: " + k);
                        id.DoAction(k);
                    }
                }
			}
		}
	}

	void checkDrop() {
        var obj_pickUp = carriedObject.GetComponent<Pickupable>();
        bool isLocked = obj_pickUp != null ? obj_pickUp.IsLocked : false;
        if (Input.GetKeyDown(KeyCode.E) || isLocked) {
			dropDaBass();
		}
	}

	void dropDaBass() {
        if (carriedObject.GetComponent<Pickupable>().IsCompound)
        {
            carrying = false;
            foreach (CompoundPickupable c in carriedObject.gameObject.GetComponent<CompoundPickupable>().Family)
            {  
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
        bool up = w > playerW;
        FourthDimension otherScript;
        otherScript = GetComponent<FourthDimension>();
        otherScript.W = w;
        playerW = w;
        gameObject.layer = 8 + playerW;
        if (carrying) {
            if (carriedObject.GetComponent<Pickupable>().IsCompound) { 
                if(up && carriedObject.GetComponent<CompoundPickupable>().CanGoWUp) {
                    foreach (CompoundPickupable c in carriedObject.GetComponent<CompoundPickupable>().Family) {
                        c.SetW(c.W + 1);                        
                      }
                }
                else if (!up && carriedObject.GetComponent<CompoundPickupable>().CanGoWDown)
                {
                    foreach (CompoundPickupable c in carriedObject.GetComponent<CompoundPickupable>().Family)
                    {
                        c.SetW(c.W - 1);
                    }
                }
                else { 
                //DROP IT LIKE ITS HOOOOT
                    dropDaBass();
                }
            }
            carriedObject.gameObject.layer = gameObject.layer;
            carriedObject.GetComponent<Pickupable>().SetW(playerW);
        }
    }

}
