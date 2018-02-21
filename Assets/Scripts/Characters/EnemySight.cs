using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    private Enemy m_Enemy;

	// Use this for initialization
	void Start( ) {
		m_Enemy = this.gameObject.GetComponentInParent<Enemy>( );
	}

    private void OnTriggerEnter2D( Collider2D _Other ){
        if( _Other.tag == "Player" ) {
            m_Enemy.SightTarget = _Other.gameObject;
        }
    }

    private void OnTriggerExit2D( Collider2D _Other ){
        if( _Other.tag == "Player" ) {
            m_Enemy.SightTarget = null;
        }
    }

}
