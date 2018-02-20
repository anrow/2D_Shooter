using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private IEnemyState m_CurrentState;

    public Vector2 GetDirection( ) {
        return isFacingRight ? Vector2.right : Vector2.left;
    }

    public override void Start( ) {

        base.Start( );
        ChangeState( new EnemyIdleState( ) );
    }

    private void Update( ) {
        m_CurrentState.Execute( );
    }

    public void ChangeState( IEnemyState _NewState ) {
        if( m_CurrentState != null ) {
            m_CurrentState.Exit( );
        }

        m_CurrentState = _NewState;

        m_CurrentState.Enter( this );
    }

    public void Move( ) {

        m_Anim.SetFloat( "Velocity", 1 );

        transform.Translate( GetDirection( ) * m_MovementSpeed * Time.deltaTime );
    }

}
