using UnityEngine;
using System.Collections;

namespace Assets.Scripts_v02.Interactives {
    public enum AnimationType {
        Translate, Rotate, Scale
    }

    public class AnimatedInteractable : Interactable {

        public AnimationType Animation;
        public Vector3 AnimationVector;
        public float AnimationValue;
        public float AnimationTime;

        private bool isAnimating;
        private bool hasOddAnimationCount;

        public override bool Interact(string parameter) {
            StartCoroutine(Animate(AnimationTime, AnimationValue, Animation));
            return true;
        }

        IEnumerator Animate(float time, float target, AnimationType aType) {
            if (!isAnimating) {
                isAnimating = true;
                var actualValue = hasOddAnimationCount ? -target : target;

                while (time > 0) {
                    switch (aType) {
                        case AnimationType.Rotate:
                            {
                                transform.Rotate(AnimationVector * actualValue * Time.deltaTime);
                                break;
                            }
                        case AnimationType.Scale:
                            {
                                transform.localScale = AnimationVector * (actualValue / (1 / time));
                                break;
                            }
                        case AnimationType.Translate:
                            {
                                transform.Translate(AnimationVector * actualValue * Time.deltaTime);
                                break;
                            }
                    }
                    time -= Time.deltaTime;
                    yield return null;
                }

                hasOddAnimationCount = !hasOddAnimationCount;
                time = AnimationTime;
                isAnimating = false;
            }
        }
    }
}
