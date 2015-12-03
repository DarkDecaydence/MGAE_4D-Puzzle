using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts_v02 {
    public class FourthDimensionSound : MonoBehaviour {
        public List<AudioSource> AudioSources;
        public float FadeTime;

        private int oldPlayerW;

        void Start() {
            foreach (AudioSource audio in GetComponentsInChildren<AudioSource>()) {
                AudioSources.Add(audio);
                audio.volume = 0;
                audio.mute = true;
            }
            AudioSources[0].mute = false;
        }

        void Update() {
            if (oldPlayerW != PickupObjectNew.PlayerW) {
                StartCoroutine(TweenAudioChange(AudioSources[oldPlayerW], AudioSources[PickupObjectNew.PlayerW], FadeTime));
            }
            oldPlayerW = PickupObjectNew.PlayerW;
        }

        IEnumerator TweenAudioChange(AudioSource current, AudioSource next, float time) {
            next.mute = false;
            while (time > 0) {
                current.volume -= (Time.deltaTime / FadeTime) * 0.5f;
                next.volume += (Time.deltaTime / FadeTime) * 0.5f;
                time -= Time.deltaTime;
                yield return null;
            }
            current.mute = true;
        }
    }
}
