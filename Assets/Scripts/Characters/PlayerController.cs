using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D m_Rb;

    private Animator m_Anim;

	private HealthController m_Health;

    private CharacterMovement m_Movement;

    // Movement Variables
    [SerializeField]
    private float m_MaxSpeed = 0;

    [SerializeField]
    private bool isFacingRight;

    private float m_Horizontal;

    private bool isRun = false;

    //Jumping Variables
    [SerializeField]
    private Transform m_GroundPoint;
    
    private bool isGrounded = false;
    
    private bool isJump = false;

    private const float POINT_RADIUS = 0.2f;
    
    [SerializeField]
    private LayerMask m_GroundLayer; 

    [SerializeField]
    private float m_JumpHeight;
    
    //Shooting Variables
    [SerializeField]
    private Transform m_ShootPoint;

    private float m_ShootRate = 0.5f;

    private float m_NextShootTime = 0f;

    private bool isShoot = false;

    public enum ENUM_PlayerState {
        Idle,
        Move,
        Jump,
        Shoot,
        Hurt,
        Dead
    }

    private ENUM_PlayerState m_State;
    

    private void Start( ) {

        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );

        m_Anim = this.gameObject.GetComponentInChildren<Animator>( );

		m_Health = this.gameObject.GetComponent<HealthController>( );

        m_Movement = new CharacterMovement( );
    }

    private void Update( ) {

        switch( m_State ) {
            case ENUM_PlayerState.Move:
                break;
            case ENUM_PlayerState.Jump:
                isGrounded = false;

                m_Movement.Jump( m_Rb, m_JumpHeight );

                m_Anim.SetBool( "IsGrounded", isGrounded );
                break;
            case ENUM_PlayerState.Shoot:
                break;
            case ENUM_PlayerState.Hurt:
                break;
            case ENUM_PlayerState.Dead:
                break;
        }

        if( Input.GetKeyDown( KeyCode.Space ) && isGrounded ) {

            isGrounded = false;

            m_Movement.Jump( m_Rb, m_JumpHeight );

            m_Anim.SetBool( "IsGrounded", isGrounded );

        }

        if( Input.GetKeyDown( KeyCode.Z ) ) {

            if( Time.time > m_NextShootTime ) {

                m_NextShootTime = Time.time + m_ShootRate;
                
                if( isFacingRight ) {

                    ObjectManager.Instance.CreateObj( ENUM_Weapon.Rocket, m_ShootPoint.position, Quaternion.Euler( new Vector3( 0, 0, 0 ) ) );

                } else if( !isFacingRight ) {

                    ObjectManager.Instance.CreateObj( ENUM_Weapon.Rocket, m_ShootPoint.position, Quaternion.Euler( new Vector3( 0, 0, 180 ) ) );
                 
                }
            }
        }

		if( m_Health.IsDead( ) ) {
			
			Dead( );
		}
			
    }

    private void FixedUpdate( ) {
        
        isGrounded = Physics2D.OverlapCircle( m_GroundPoint.position, POINT_RADIUS, m_GroundLayer );

        m_Anim.SetBool( "IsGrounded", isGrounded );

        m_Anim.SetFloat( "JumpVelocity", m_Rb.velocity.y );

        m_Horizontal = Input.GetAxis( "Horizontal" );

        m_Anim.SetFloat( "Velocity", Mathf.Abs( m_Horizontal ) );
    
        m_Movement.Move( m_Rb, m_MaxSpeed, m_Horizontal );
        
        if( m_Horizontal > 0 && !isFacingRight || m_Horizontal < 0 && isFacingRight  ) {
            isFacingRight = !isFacingRight;
            m_Movement.Flip( this.gameObject );
        }
    }

    public void Dead( ) {
        ObjectManager.Instance.CreateObj( ENUM_Fx.DeathFx, transform.position );
        Destroy( this.gameObject );
    } 

    private void HandleInput( ) {
        if( m_Horizontal != 0  ) {
            isRun = true;
        }
        if( Input.GetKeyDown( KeyCode.Space ) && isGrounded ) {
            isJump = true;
        }
        if( Input.GetKeyDown( KeyCode.Z ) ) {
            isShoot = true;
        }
    }
}
