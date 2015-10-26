using UnityEngine;
using System.Collections;

public class PlayerController : Satellite
{
    public float speed;
    public float turnSpeed;

    void FixedUpdate()
    {
     
        float yaw = Input.GetAxis("Yaw");
        float roll = Input.GetAxis("Roll");
        float pitch = Input.GetAxis("Pitch");

        Vector3 turn = new Vector3(pitch, yaw, roll);
        transform.Rotate(turn);

        //rb.AddTorque(turn*turnSpeed);

        float power = Input.GetAxis("Power");

        rb.AddForce(transform.forward * speed * power);
        
    }
}
