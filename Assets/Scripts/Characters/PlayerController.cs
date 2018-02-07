using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_PlayerState {
	Idle,
	Move,
	Jump,
	Shoot,
	Hurt,
	Dead
}

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

    //Jumping Variables
    [SerializeField]
    private Transform m_GroundPoint;
    
    private bool isGrounded = false;

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

    private ENUM_PlayerState m_State;
    
	public ENUM_PlayerState State {
		get{ return m_State; }
		set{ m_State = value; }
	}

    private void Start( ) {

        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );

        m_Anim = this.gameObject.GetComponentInChildren<Animator>( );

		m_Health = this.gameObject.GetComponent<HealthController>( );

        m_Movement = new CharacterMovement( );
    }

    private void Update( ) {
		
		if( m_Health.IsDead( ) ) {
			m_State = ENUM_PlayerState.Dead;
		}

		if( m_Health.IsHurt == true ) {
			m_State = ENUM_PlayerState.Hurt;
		}

		HandleInput( );

        switch( m_State ) {
			case ENUM_PlayerState.Idle:
				m_Health.IsHurt = false;
				SpriteRenderer theRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>( );
				theRenderer.color = new Color( 1, 1, 1, 1 );
				break;
			case ENUM_PlayerState.Move:
				Move( );
                break;
			case ENUM_PlayerState.Jump:
				Jump( );
                break;
			case ENUM_PlayerState.Shoot:
				Shoot( );
                break;
            case ENUM_PlayerState.Hurt:
				Invincible( );
                break;
			case ENUM_PlayerState.Dead:
				Dead( );
                break;
        }
		Debug.Log( m_State );	
    }

    private void FixedUpdate( ) {
        
        isGrounded = Physics2D.OverlapCircle( m_GroundPoint.position, POINT_RADIUS, m_GroundLayer );

		m_Horizontal = Input.GetAxis( "Horizontal" );

        m_Anim.SetBool( "IsGrounded", isGrounded );

        m_Anim.SetFloat( "JumpVelocity", m_Rb.velocity.y );

        //m_Anim.SetFloat( "Velocity", Mathf.Abs( m_Horizontal ) );
    
        //m_Movement.Move( m_Rb, m_MaxSpeed, m_Horizontal );
        
        /*if( m_Horizontal > 0 && !isFacingRight || m_Horizontal < 0 && isFacingRight  ) {
            isFacingRight = !isFacingRight;
            m_Movement.Flip( this.gameObject );
        }*/
    }

	private void HandleInput( ) {
		if( m_Horizontal != 0 ) {
			m_State = ENUM_PlayerState.Move;
		}

		if( Input.GetKeyDown( KeyCode.Space ) && isGrounded ) {
			m_State = ENUM_PlayerState.Jump;
		}

		if( Input.GetKeyDown( KeyCode.Z ) ) {
			m_State = ENUM_PlayerState.Shoot;
		}
	}

    public void Dead( ) {
        ObjectManager.Instance.CreateObj( ENUM_Fx.DeathFx, transform.position );
        Destroy( this.gameObject );
    } 
		
	private void Jump( ) {
		
		isGrounded = false;

		m_Movement.Jump( m_Rb, m_JumpHeight );

		m_Anim.SetBool( "IsGrounded", isGrounded );
	}

	private void Shoot( ) {
		if( Time.time > m_NextShootTime ) {

			m_NextShootTime = Time.time + m_ShootRate;

			if( isFacingRight ) {

				ObjectManager.Instance.CreateObj( ENUM_Weapon.Rocket, m_ShootPoint.position, Quaternion.Euler( Vector3.zero ) );

			} else if( !isFacingRight ) {

				ObjectManager.Instance.CreateObj( ENUM_Weapon.Rocket, m_ShootPoint.position, Quaternion.Euler( new Vector3( 0, 0, 180 ) ) );

			}
		}
	}

	private void Move( ) {

		if( m_Horizontal > 0 && !isFacingRight || m_Horizontal < 0 && isFacingRight  ) {
			isFacingRight = !isFacingRight;
			m_Movement.Flip( this.gameObject );
		}

		m_Movement.Move( m_Rb, m_MaxSpeed, m_Horizontal );

		m_Anim.SetFloat( "Velocity", Mathf.Abs( m_Horizontal ) );

	}

	private void Invincible( ) {
		
		float theInvincibleTime = Time.time;

		SpriteRenderer theRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>( );
	
		if( theInvincibleTime > 10f ) {
			m_State = ENUM_PlayerState.Idle;
		} else {
			theRenderer.color = Color.Lerp( new Color( 1, 1, 1, 0.2f ), new Color( 1, 1, 1, 1 ), Mathf.PingPong( Time.time, 0.1f ) );
		}

		Debug.Log( theInvincibleTime );
	}
}
