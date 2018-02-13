using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByDamage : MonoBehaviour {

    private HealthController m_Health;

    private void Start( ) {
        m_Health = this.gameObject.GetComponent<HealthController>( );
	}

    private void Update( ) {
        if( m_Health.IsDead( ) ) {
            ObjectManager.Instance.CreateObj( ENUM_Fx.EnemyExplosionFx, transform.position );
            Destroy( this.gameObject );
            if( Random.Range( 0, 10 ) >= 5 ) {
                ObjectManager.Instance.CreateObj( ENUM_Item.Heart, transform.position, transform.rotation );
            }
        }
    }
}
