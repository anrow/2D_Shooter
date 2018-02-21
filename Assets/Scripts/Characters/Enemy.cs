using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private IEnemyState m_CurrentState;

    private GameObject m_SightTarget;

    public GameObject SightTarget {
        get { return m_SightTarget; }
        set { m_SightTarget = value; }
    }

    public Vector2 GetDirection( ) {
        return isFacingRight ? Vector2.right : Vector2.left;
    }

    [SerializeField]
    private float m_AttackRange;

    public bool InAttackRange {
        get {
            if( m_SightTarget != null ) {
                return Vector2.Distance( transform.position, m_SightTarget.transform.position ) <= m_AttackRange;
            }
            return false;
        }
    }

    public override void Start( ) {

        base.Start( );
        ChangeState( new EnemyIdleState( ) );
    }

    private void Update( ) {

        m_CurrentState.Execute( );

        LookAtTarget( );
    }

    private void LookAtTarget( ) {
        if( m_SightTarget != null ) {

            float theDirX = m_SightTarget.transform.position.x - transform.position.x;

            if( theDirX < 0 && isFacingRight || theDirX > 0 && !isFacingRight ) {
                ChangeDirection( );
            }
        }
    }

    public void ChangeState( IEnemyState _NewState ) {
        if( m_CurrentState != null ) {
            m_CurrentState.Exit( );
        }

        m_CurrentState = _NewState;

        m_CurrentState.Enter( this );
    }

    public void Move( ) {

        float m_AnimationSpeed = m_SightTarget == null ? 1 : 5;

        m_MovementSpeed = m_SightTarget == null ? 2 : 6;

        if( !isAttack  ) {

            m_Anim.SetFloat( "Velocity", m_AnimationSpeed );

            transform.Translate( GetDirection( ) * ( m_MovementSpeed * Time.deltaTime ) );
        }
    }

}
