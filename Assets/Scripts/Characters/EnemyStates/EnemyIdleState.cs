using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState {

    private Enemy m_Enemy;

    private float m_Timer;

    private const float m_Duration = 5f;

    public void Enter( Enemy _Enemy ) {
        this.m_Enemy = _Enemy;
    }

    public void Execute( ) {
        Idle( );
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
