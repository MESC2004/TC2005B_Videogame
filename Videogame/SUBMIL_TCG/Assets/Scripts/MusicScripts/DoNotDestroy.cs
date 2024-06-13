using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    public AudioClip originalMusic; // Assign in the inspector
    public AudioClip specialSceneMusic; // Assign in the inspector
    public string specialSceneName; // Name of the special scene
    public string titleSceneName; // Name of the title scene

    private AudioSource audioSource;
    private bool isSpecialSceneMusicPlaying = false;

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = originalMusic;
        audioSource.loop = true;
        audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == specialSceneName)
        {
            if (!isSpecialSceneMusicPlaying)
            {
                audioSource.clip = specialSceneMusic;
                audioSource.Play();
                isSpecialSceneMusicPlaying = true;
            }
        }
        else if (scene.name == titleSceneName)
        {
            if (isSpecialSceneMusicPlaying)
            {
                audioSource.clip = originalMusic;
                audioSource.Play();
                isSpecialSceneMusicPlaying = false;
            }
        }
        else
        {
            if (isSpecialSceneMusicPlaying)
            {
                audioSource.clip = originalMusic;
                audioSource.Play();
                isSpecialSceneMusicPlaying = false;
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

