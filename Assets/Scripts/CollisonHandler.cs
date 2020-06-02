using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{
    [Tooltip("In second")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        deathFX.SetActive(true);
        StartDeathSeq();
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSeq()
    {
        SendMessage("OnDeath");
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

}
