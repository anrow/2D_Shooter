using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {

    [SerializeField]
    private Collider2D m_TargetCol;

    private Collider2D[ ] m_Colliders;

    private void Awake( ) {

        m_Colliders = this.gameObject.GetComponents<Collider2D>( );

        foreach( Collider2D m_Collider in m_Colliders ) {
            Physics2D.IgnoreCollision( m_Collider, m_TargetCol, true );
        }

        
    }

}
