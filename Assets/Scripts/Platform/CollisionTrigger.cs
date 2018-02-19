using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour {

    [SerializeField]
    private BoxCollider2D m_Collider;

    [SerializeField]
    private BoxCollider2D m_Trigger;

    private CapsuleCollider2D m_PlayerCollider;

	// Use this for initialization
	void Start () {

        m_PlayerCollider = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<CapsuleCollider2D>( );

		Physics2D.IgnoreCollision( m_Collider, m_Trigger, true );

        Debug.Log( m_PlayerCollider );
	}

    private void OnTriggerEnter2D( Collider2D _Other ) {

        if( _Other.gameObject.tag == "Player" ) {
            Physics2D.IgnoreCollision( m_Collider, m_PlayerCollider, true );
        }

    }

    private void OnTriggerExit2D( Collider2D _Other ) {

        if( _Other.gameObject.tag == "Player" ) {
            Physics2D.IgnoreCollision( m_Collider, m_PlayerCollider, false );
        }

    }

}
