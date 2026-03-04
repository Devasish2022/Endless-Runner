using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private GameObject endMenuOverlay;
    [SerializeField] private AudioSource audioSource;

    const String OBSTACLE_TAG = "Obstacle";

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(OBSTACLE_TAG))
        {
            audioSource.Stop();
            Time.timeScale = 0f;
            endMenuOverlay.SetActive(true);
        }
    }
}
