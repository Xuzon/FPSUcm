using UnityEngine;
using System.Collections;

public class HealthSlider : MonoBehaviour {


    public Health m_health;

    private UnityEngine.UI.Slider m_slider;

	void Start () {

        m_slider = GetComponent<UnityEngine.UI.Slider>();
        if(m_slider != null)
        {
            m_slider.minValue = 0;
            m_slider.maxValue = m_health.m_health;
            m_slider.value = m_health.m_health;
        }

    }
	
	void Update ()
    {
        m_slider.value = m_health.m_CurrentHealth;
    }
}
