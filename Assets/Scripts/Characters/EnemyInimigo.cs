using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInimigo : MonoBehaviour {
    
    [SerializeField]
    private float m_Speed;

    private Rigidbody2D m_Rb;

    private CharacterMovement m_Movement;

    private void Start( ) {

        m_Movement = new CharacterMovement( );

        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );
    }
    private void Update( ) {
        m_Movement.Move( m_Rb, m_Speed );
    }
    private void OnTriggerEnter2D( Collider2D _Other ){
        if( _Other.gameObject.layer == LayerMask.NameToLayer( "Wall" ) ) {
            m_Movement.Flip( this.gameObject );
            m_Speed = -m_Speed;
        }
    }

    private void OnCollisionEnter2D( Collision2D _Other ){
        if( _Other.gameObject.layer == LayerMask.NameToLayer( "MovingGround" ) ) {
            this.gameObject.transform.parent = _Other.transform;
        }
    }
    

}
