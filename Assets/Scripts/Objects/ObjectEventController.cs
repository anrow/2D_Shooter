using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventController : MonoBehaviour {
	
	[SerializeField]
	private HealthController m_HealthController;

	[SerializeField]
	private DamageController m_DamageController;

	void Start( ) {
		m_HealthController = this.gameObject.GetComponent<HealthController>( );
		m_DamageController = this.gameObject.GetComponent<DamageController>( );
	}

	void Update( ) {
		if( m_HealthController.IsDead () ) {
			Destroy (this.gameObject);
		}
	}
}
