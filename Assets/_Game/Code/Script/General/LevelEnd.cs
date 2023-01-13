using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {
    public AudioClip NextLevelSFX;
    public int NextLevelIndex = 0;

    private void OnTriggerEnter(Collider other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null) {
            AudioManager.Instance.PlayAudio(NextLevelSFX, transform.position);
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel() {
        GameManager.Instance.UI.NextLevelTransition();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LoadLevel(NextLevelIndex);
    }

}
