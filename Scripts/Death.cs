using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public AudioClip m_deathSound;

    private AudioSource m_audio;
    private GameObject m_GameManager;
    protected GameManager manager;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
    }

    public virtual void OnDeath()
    {

        m_audio.clip = m_deathSound;
        m_audio.Play();
        manager?.RespawnPlayer();
    }
}
