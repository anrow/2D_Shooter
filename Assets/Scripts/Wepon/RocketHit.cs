using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHit : MonoBehaviour {
    
    [SerializeField]
    private GameObject mGo_Explosion;

    [SerializeField]
    private float m_Damage;

    private RocketController m_RocketController;

    private void Awake( ) {
        m_RocketController = this.gameObject.GetComponentInParent<RocketController>( );
    }

    private void OnTriggerEnter2D( Collider2D _Other ){

        if( _Other.gameObject.layer == LayerMask.NameToLayer( "ShootAble" ) ) {

            m_RocketController.RemoveSpeed( );

            ObjectManager.Instance.CreateObj( ENUM_Fx.RocketExplosionFx, transform.position );

            Destroy( m_RocketController.gameObject );
            
            if( _Other.gameObject.tag == "Enemy" ) {

                HealthController theEnemyHealth = _Other.gameObject.GetComponent<HealthController>( );

                theEnemyHealth.AddDamage( m_Damage );

                if( theEnemyHealth.IsDead( ) ) {
                    Destroy( _Other.gameObject );
                }
            }
        }
    }
}
