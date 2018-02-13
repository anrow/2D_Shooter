using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

	[SerializeField]
	private float m_Damage;

	[SerializeField]
	private float m_DamageRate;

	[SerializeField]
	private float m_PushBackForce;

	private float m_NextDamageTime;

	// Use this for initialization
	void Start () {
		m_NextDamageTime = 0;
	}

    void OnTriggerEnter2D( Collider2D _Other ) {

		if( _Other.gameObject.tag == "Player" ) {

			PlayerController theplayer = _Other.gameObject.GetComponent<PlayerController>( );

			HealthController thePlayerHealth = _Other.gameObject.GetComponent<HealthController>( );

            if( !theplayer.IsInvincible( ) ) {
			    thePlayerHealth.AddDamage( m_Damage );
                PushBack( _Other.transform );
            }

			m_NextDamageTime = Time.time + m_DamageRate;
		}
	}

	void OnTriggerStay2D( Collider2D _Other ) {

		if( _Other.gameObject.tag == "Player" ) {

            PlayerController theplayer = _Other.gameObject.GetComponent<PlayerController>( );

			HealthController thePlayerHealth = _Other.gameObject.GetComponent<HealthController>( );

			if( !theplayer.IsInvincible( ) ) {
			    thePlayerHealth.AddDamage( m_Damage );
                PushBack( _Other.transform );
            }
		}

	}

	void PushBack( Transform _Target ) {

		Vector2 thePushDirection = new Vector2( _Target.position.x - transform.position.x, _Target.position.y - transform.position.y ).normalized;

        float thePushBackForce = new Vector2( _Target.position.x - transform.position.x, _Target.position.y - transform.position.y ).sqrMagnitude;

		thePushDirection *= thePushBackForce + m_PushBackForce;

		Rigidbody2D theTargetRb = _Target.gameObject.GetComponent<Rigidbody2D>( );

		theTargetRb.velocity = Vector2.zero;

		theTargetRb.AddForce( thePushDirection, ForceMode2D.Impulse );
	}
}
