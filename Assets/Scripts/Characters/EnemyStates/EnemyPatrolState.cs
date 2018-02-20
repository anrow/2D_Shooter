using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState {
    
    private Enemy m_Enemy;

    private float m_Timer;

    private float m_Duration = 10f;

    public void Enter( Enemy _Enemy ) {
        this.m_Enemy = _Enemy;
    }

    public void Execute( ) {
    }

    public void Exit( ) {
    }

    public void OnTriggerEnter2D( Collider2D _Otehr ) {
    }

    private void Patrol( ) {

        m_Timer += Time.deltaTime;

        if( m_Timer >= m_Duration ) {

            m_Enemy.ChangeState( new EnemyIdleState( ) );

        }
    }
}
