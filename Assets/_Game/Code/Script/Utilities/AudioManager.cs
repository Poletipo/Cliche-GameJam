using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager _instance = null;

    public static AudioManager Instance {
        get {
            if (_instance == null) {
                GameObject gmObject = GameObject.Find("AudioManager");

                _instance = gmObject.GetComponent<AudioManager>();
            }

            return _instance;
        }
    }

    public void PlayAudio(AudioClip clip, Vector3 position, float volume = 1) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }




}
