using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {

    [SerializeField]
    private int m_HealthVaule;

    private void OnTriggerEnter2D( Collider2D _Other ){

        if( _Other.tag == "Player" ) {

            HealthController thePlayerHealth = _Other.gameObject.GetComponent<HealthController>( );

            thePlayerHealth.AddHealth( m_HealthVaule );

            Destroy( this.gameObject );
        }
    }
}
