using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreenPanel : MonoBehaviour {

	[SerializeField]
	private HealthController m_PlayerHealth;

	[SerializeField]
	private Image m_DamageScreenImage;

	[SerializeField]
	private Color m_DangerColor = new Color( 1f, 1f, 1f, 1f );

	[SerializeField]
	private float m_SmoothColorRate;

	// Use this for initialization
	void Start () {
		
		m_DamageScreenImage = this.gameObject.GetComponent<Image> ();

		m_PlayerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthController> ();
	
	}
	// Update is called once per frame
	void Update( ) {
		if( !m_PlayerHealth.IsDead( ) ) {
		    if( m_PlayerHealth.CurrentHealth <= 5 ) {

                float alfa = Mathf.PingPong( Time.time, 1 );
                m_DamageScreenImage.color = new Color( 1, 1, 1, alfa );

            } else {
                m_DamageScreenImage.color = Color.clear;
            }
        }
    }
}
