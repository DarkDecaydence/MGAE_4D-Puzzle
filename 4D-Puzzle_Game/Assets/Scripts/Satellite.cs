using UnityEngine;
using System.Collections;

public class Satellite : MonoBehaviour {

    protected Rigidbody rb;

    public Rigidbody[] satellites;

    private float G;
    
    void Start () {
        G = 1.5f; //6.674f;
        rb = GetComponent<Rigidbody>();
        float x = Random.Range(-10.0f, 10.0f);
        float y = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        Vector3 start = new Vector3(x, y, z);
        rb.AddForce(start * 100);
    }
	
	void Update () {
        foreach (Rigidbody s in satellites)
        {
            Vector3 offset = s.transform.position - rb.position;
            float force = G * s.mass * rb.mass / ((offset.magnitude) * (offset.magnitude));
            float factor = offset.magnitude / force;
            rb.AddForce(offset / factor);
        }
	}
}
