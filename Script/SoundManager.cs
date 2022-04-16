using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [Header("Variables")]
    [Range(0, 1.5f)] [SerializeField] private float timeToVolumeMax;
    [HideInInspector] public bool inForest;
    [HideInInspector] public bool inHouse;

    [Header("Clips")]
    [SerializeField] private AudioClip ricardoHouse;
    [SerializeField] private AudioClip musicForest;
    [Space]
    [SerializeField] private AudioClip playerHurt;
    [SerializeField] private AudioClip playerDead;
    [SerializeField] private AudioClip playerAttack;
    [Space]
    [SerializeField] private AudioClip enemyDead;

    [Header("Component")]
    private AudioSource music;
    private AudioSource sfx;

    private void Start()
    {
        music = GetComponent<AudioSource>();
        sfx = GameObject.Find("SFX").GetComponent<AudioSource>();

        music.volume = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
        sfx.volume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }
    public void ChangeMusic()
    {
        music.volume = 0;

        if (inForest)
        {
            music.Stop();
            music.clip = musicForest;
            music.Play();
        }
        if (inHouse)
        {
            music.Stop();
            music.clip = ricardoHouse;
            music.Play();
        }

        StartCoroutine(MoveVolume());
    }
    private IEnumerator MoveVolume(bool direction = true)
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
        volume *= 100;

        // TRUE = En Subida
        if (direction)
        {
            for (int i = 0; i < volume; i++)
            {
                music.volume += 0.01f;
                yield return new WaitForSeconds(timeToVolumeMax / volume);
            }
        }
        else
        {
            // FALSE = En Bajada
            for (int i = 0; i < volume; i++)
            {
                music.volume -= 0.01f;
                yield return new WaitForSeconds(timeToVolumeMax / volume);
            }

            music.Stop();
        }
    }
    public void StopMusic()
    {
        inForest = false;
        inHouse = false;

        StartCoroutine(MoveVolume(false));
    }
    public void PlayerDead()
    {
        sfx.PlayOneShot(playerDead);
    }
    public void PlayerAttack()
    {
        sfx.PlayOneShot(playerAttack);
    }
    public void PlayerHurt()
    {
        sfx.PlayOneShot(playerHurt);
    }
    public void DeadEnemies()
    {
        sfx.PlayOneShot(enemyDead);
    }
}
