using UnityEngine;
using System.Collections;
namespace Assets.Scripts_v02.Interactives
{
    public class Elevator : MonoBehaviour, IUsable
    {
        private bool Enabled = true;
        private bool isOn = false;
        float curTime = 0.0f;
        public float travelTime;
        private Vector3 A;
        private Vector3 B;

        public bool Interact(string parameter) {
            if(Enabled) isOn = true;
            return isOn;
        }

        public void Start() {
            A = gameObject.transform.position;
            B = A + new Vector3(0, 3.9f, 0);
        }

        // Update is called once per frame
        public void Update()
        {
            if (isOn)
            {
                curTime = curTime + Time.deltaTime;
                float lerpTime = curTime / travelTime;

                var result = Vector3.Lerp(A, B, lerpTime);
                gameObject.transform.position = new Vector3(result.x, result.y, result.z);
            }
           /* if (gameObject.transform.position.Equals(B))
            {
                isOn = false;
                curTime = 0;
            }*/
        }
    }
}
