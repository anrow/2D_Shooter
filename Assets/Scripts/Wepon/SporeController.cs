using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeController : MonoBehaviour {
    
    private Rigidbody2D m_Rb;

    [SerializeField]
    private float m_MaxSpeed;

    [SerializeField]
    private float m_MinSpeed;

    [SerializeField]
    private float m_Angle;

    [SerializeField]
    private float m_Torque;

	[SerializeField]
	private float m_Damage;

    private void Awake( ) {
		
        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );

        m_Rb.AddForce( new Vector2( Random.Range( -m_Angle, m_Angle ), Random.Range( m_MinSpeed, m_MaxSpeed ) ), ForceMode2D.Impulse );

        m_Rb.AddTorque( Random.Range( -m_Torque, m_Torque ) );
    }

	private void OnTriggerEnter2D( Collider2D _Other ) {

		if( !_Other.gameObject.GetComponent<Collider2D>( ).isTrigger ) {
			/*if( _Other.gameObject.tag == "player" ) {
				HealthController thePlayerHealth = _Other.gameObject.GetComponent<HealthController>( );
				thePlayerHealth.AddDamage( m_Damage );
			}*/

			ObjectManager.Instance.CreateObj( ENUM_Fx.ExplosionFx, transform.position );

			Destroy( this.gameObject );
		}
	}
}

