using UnityEngine;
using System.Collections;

/**
 * 4th dimensional controller.
 */
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
        float moveW = Input.GetAxis("Wertical");
        MoveFloatW(moveW*0.5f);
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