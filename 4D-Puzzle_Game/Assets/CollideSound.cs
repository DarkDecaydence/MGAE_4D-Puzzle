using UnityEngine;
using System.Collections;

public class CollideSound : MonoBehaviour {

    public AudioSource source;

    void OnCollisionEnter(Collision other) {
        source.PlayOneShot(source.clip);
    }
}
