using System.Collections;
using UnityEngine;

namespace Assets.Scripts_v02.Interactives {
    public class InteractableDoor : MonoBehaviour, IUsable {

        public Vector3 RotationAxis;
        public string RequiredKey;
        public bool IsOpen;
        public float OpenTime;
        public float Angle;

        public bool Interact(string parameter) {
            if (parameter == RequiredKey) {
                if (IsOpen) Rotate(OpenTime, -Angle);
                else Rotate(OpenTime, Angle);
                return true;
            } else return false;
        }

        IEnumerator Rotate(float time, float angle) {
            while (time > 0) {
                transform.Rotate(RotationAxis * -Angle * Time.deltaTime);
                time -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
