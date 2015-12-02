using UnityEngine;
using System;
using System.Collections;

public class GoToLevel : MonoBehaviour {

    public string KeyName;
    public string LevelName;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == KeyName || string.IsNullOrEmpty(KeyName)) {
            Application.LoadLevel(LevelName);
        }
    }
}
