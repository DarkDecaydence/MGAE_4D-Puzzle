using UnityEngine;
using System.Collections;

public class PController : MonoBehaviour {

    public static int playerW;
    public float speed;
    private float floatW;
    private Rigidbody rb;
    public GameObject obj;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Yaw");
        float moveVertical = Input.GetAxis("Power");
        float moveW = Input.GetAxis("Pitch");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        MoveFloatW(moveW*0.5f);
        
        /*
        if (moveW > 0 && playerW < 4) {
            SetW(playerW + 1);
        } else if (moveW < 0 && playerW > 0) {
            SetW(playerW - 1);
        }*/
        rb.AddForce(movement * speed);
    }

    void SetW(int w)
    {
        FourthDimension otherScript;
        otherScript = GetComponent<FourthDimension>();
        otherScript.W = w;
        playerW = w;
        obj.layer = 8+playerW;
    }

    void MoveFloatW(float move) {
        floatW = floatW + move;
        if (floatW < 0)
        {
            floatW = 0.0f;
        }
        else if (floatW > 4.9f) {
            floatW = 4.9f;
        }
        SetW((int)floatW);
    }


}