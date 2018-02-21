using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState {

    private Enemy m_Enemy;

    private float m_AttackTime;

    private float m_AttackCoolDownTime = 0.1f;

    private bool canAttack;

    public void Enter( Enemy _Enemy ) {
        this.m_Enemy = _Enemy;
    }

    public void Execute( ) {

        Attack( );

        if( !m_Enemy.InAttackRange ) {
            m_Enemy.ChangeState( new EnemyRangedState( ) );
        }
        else if( m_Enemy.SightTarget != null ) {
            m_Enemy.ChangeState( new EnemyIdleState( ) );
        }
    }

    public void Exit( ) {
    }

    public void OnTriggerEnter2D( Collider2D _Otehr ) {
    }

    private void Attack( ) {

        m_AttackTime += Time.deltaTime;

        if( m_AttackTime >= m_AttackCoolDownTime ) {
            canAttack = true;
            m_AttackTime = 0;
        }
        if( canAttack ) {
            canAttack = false;
            m_Enemy.Anim.SetTrigger( "Attack" );
        }
    }
}
