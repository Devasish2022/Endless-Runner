using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private GameObject endMenuOverlay;
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Stop();
        Time.timeScale = 0f;
        endMenuOverlay.SetActive(true);
    }
}
