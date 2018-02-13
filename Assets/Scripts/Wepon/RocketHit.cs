using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHit : MonoBehaviour {
    
    [SerializeField]
    private float m_Damage;

    private RocketController m_RocketController;

    private void Awake( ) {
        m_RocketController = this.gameObject.GetComponentInParent<RocketController>( );
    }

    private void OnTriggerEnter2D( Collider2D _Other ){

        if( _Other.gameObject.tag != "Player" ) {

            if( _Other.gameObject.layer == LayerMask.NameToLayer( "ShootAble" ) ) {

                m_RocketController.RemoveSpeed( );
                
                HealthController theEnemyHealth = _Other.gameObject.GetComponent<HealthController>( );

                if( theEnemyHealth == null ) {
                    theEnemyHealth = _Other.gameObject.GetComponentInParent<HealthController>( );
                }
                if( theEnemyHealth == null ) {
                    theEnemyHealth = _Other.gameObject.GetComponentInChildren<HealthController>( );
                }

                theEnemyHealth.AddDamage( m_Damage );

                Destroy( m_RocketController.gameObject );
                ObjectManager.Instance.CreateObj( ENUM_Fx.ExplosionFx, transform.position );
            }
        }
    }
}
