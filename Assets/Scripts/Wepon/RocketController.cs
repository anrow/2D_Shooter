using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {
    
    private Rigidbody2D m_Rb;

    [SerializeField]
    private float m_Speed;

    private void Awake( ) {
        
        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );

        if( transform.localRotation.z > 0 ) {

            m_Rb.AddForce( Vector2.left * m_Speed, ForceMode2D.Impulse );

        } else {
            m_Rb.AddForce( Vector2.right * m_Speed, ForceMode2D.Impulse );
        }
    }

    public void RemoveSpeed( ) {
        m_Rb.velocity = new Vector2( 0, 0 );
    }

}
