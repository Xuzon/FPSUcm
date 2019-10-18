﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    public float m_health;

    public float m_CurrentHealth { get; private set; }


    // Use this for initialization


    void Start () {
        ResetHealth();
    }

    public float CurrentHelth
    {
        get { return m_CurrentHealth; }
    }

    public void ResetHealth()
	{
        m_CurrentHealth = m_health;
    }

    /// <summary>
    /// Mensaje que aplica el daño y lanza el mensaje OnDeath cuando la salud es menor que 0.
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(float amount)
    {

        ///  // ## TO-DO 1 si la salud inicial es menor que 0 enviar mensaje void OnDeath() por si a alguien le interesa..
        m_CurrentHealth -= amount;
        if(m_CurrentHealth <= 0)
        {
            gameObject.SendMessage("OnDeath");
        }

    }

}
