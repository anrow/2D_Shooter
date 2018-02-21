using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedState : IEnemyState {

    private Enemy m_Enemy;

    public void Enter( Enemy _Enemy ) {
        this.m_Enemy = _Enemy;
    }

    public void Execute( ) {

        if( m_Enemy.InAttackRange ) {
            m_Enemy.ChangeState( new EnemyAttackState( ) );
        }

        else if( m_Enemy.SightTarget != null ) {

            m_Enemy.Move( );

        }

        else {
            m_Enemy.ChangeState( new EnemyIdleState( ) );
        }
    }

    public void Exit( ) {
    }

    public void OnTriggerEnter2D( Collider2D _Otehr ) {
    }
}
