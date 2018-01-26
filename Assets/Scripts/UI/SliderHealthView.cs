using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealthView : MonoBehaviour {

	[SerializeField]
	private Slider m_Slider;

	[SerializeField]
	private HealthController m_Target;

	// Use this for initialization
	void Start () {
		if (m_Slider == null) {
			m_Slider = this.gameObject.GetComponent<Slider> ();
		}
		if (m_Slider == null) {
			m_Slider = this.gameObject.GetComponentInChildren<Slider> ();
		}

		if (this.gameObject.name == "PlayerHealthPanel") {
			m_Target = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthController> ();
		} else if (this.gameObject.name == "SliderEnemyHealth") {
			m_Target = this.GetComponentInParent<HealthController> ();
		}

		m_Slider.maxValue = m_Target.MaxHealth;

		m_Slider.minValue = 0;

	}
	
	// Update is called once per frame
	void Update () {
		m_Slider.value = m_Target.CurrentHealth;
	}
}
