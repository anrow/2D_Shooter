using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreenPanel : MonoBehaviour {

	[SerializeField]
	private HealthController m_Player;

	[SerializeField]
	private Image m_DamageScreenImage;

	[SerializeField]
	private Color m_DamageColor = new Color( 0, 0, 0, 1f );

	[SerializeField]
	private Color m_DangerColor = new Color( 0.5f, 0.5f, 0.5f, 1 );

	[SerializeField]
	private float m_SmoothColorRate;

	// Use this for initialization
	void Start () {
		
		m_DamageScreenImage = this.gameObject.GetComponent<Image> ();

		m_Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthController> ();
	
	}
	// Update is called once per frame
	void Update () {
		
		if ( m_Player.IsHurt == true ) {
			m_DamageScreenImage.color = m_DamageColor;
		} else if( m_Player.CurrentHealth <= 3 ) {
			//m_DamageScreenImage.color = m_DangerColor;
		}else {
			m_DamageScreenImage.color = Color.Lerp( m_DamageScreenImage.color, Color.clear, m_SmoothColorRate * Time.deltaTime );
		}
		m_Player.IsHurt = false;
	}
}
