using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public int nextLevel = 0;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            StartCoroutine(NextLevel());
        }
    }


    IEnumerator NextLevel()
    {
        GameManager.Instance.UI.NextLevelTransition();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LoadLevel(nextLevel);

    }


}
