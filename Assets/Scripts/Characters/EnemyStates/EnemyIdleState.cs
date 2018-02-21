using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState {

    private Enemy m_Enemy;

    private float m_Timer;

    private float m_Duration = 0.5f;

    public void Enter( Enemy _Enemy ) {
        this.m_Enemy = _Enemy;
    }

    public void Execute( ) {
        Idle( );

        if( m_Enemy.SightTarget != null ) {
            m_Enemy.ChangeState( new EnemyPatrolState( ) );
        }
    }

    public void Exit( ) {
    }

    public void OnTriggerEnter2D( Collider2D _Otehr ) {
    }

    private void Idle( ) {

        m_Enemy.Anim.SetFloat( "Velocity", 0 );

        m_Timer += Time.deltaTime;

        if( m_Timer >= m_Duration ) {
            m_Enemy.ChangeState( new EnemyPatrolState( ) );
        }
    }
}
