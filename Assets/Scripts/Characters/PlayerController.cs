using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_PlayerState {
	Idle,
	Hurt,
    Invincible,
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

    private bool isMove;

    private bool isOnMovingPlatform;

    public bool IsOnMovingPlatform( ) {
        return isOnMovingPlatform;
    }

    //Jumping Variables
    [SerializeField]
    private Transform m_GroundPoint;
    
    private bool isGrounded = false;

    private bool isJump;

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

    private bool isShoot;

    //Chop Variables
    private bool isChop = false;

    //JumpKick Variables
    private bool isJumpKick = false;

    [SerializeField]
    private float m_JumpKickForce;

    //Death Variables
    private bool isDead( ) {
        return m_Health.IsDead( );
    }  

    //Invincible Varables
    private bool isInvincible = false;

    public bool IsInvincible( ) {
        return isInvincible;
    }

    private void Start( ) {
        
        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );

        m_Anim = this.gameObject.GetComponentInChildren<Animator>( );

		m_Health = this.gameObject.GetComponent<HealthController>( );

        m_Movement = new CharacterMovement( );
    }

    private void Update( ) {
        
		HandleInput( );
        
        if( isDead( ) ) {
            Dead( );
        }
        if( m_Health.IsHurt && !isDead( ) ) {
            
            StartCoroutine( SetInvincible( ) );
            m_Health.IsHurt = false;
        }
        
    }

    private void FixedUpdate( ) {
 
        m_Horizontal = Input.GetAxis( "Horizontal" );

        isGrounded = Physics2D.OverlapCircle( m_GroundPoint.position, POINT_RADIUS, m_GroundLayer );

        if( isMove ) {
            Move( );
        }

        if( isJump ) {
            Jump( );     
        }
        if( isChop ) {
            m_Anim.SetTrigger( "Chop" );
        }
        if( isShoot ) {
            Shoot( );
        }
        if( isJumpKick ) {
            JumpKick( );
        }

        m_Anim.SetBool( "IsGrounded", isGrounded );

        m_Anim.SetFloat( "JumpVelocity", m_Rb.velocity.y );

        ResetInputVaule( );

    }

    private void HandleInput( ) {
        
        if( m_Horizontal != 0 && !isChop ) {
            isMove = true;
        }
        if( Input.GetKeyDown( KeyCode.Space ) && isGrounded ) {
            isJump = true;
        }
        if ( Input.GetKeyDown( KeyCode.Z ) ) {
            isShoot = true;
        }
        if( Input.GetKeyDown( KeyCode.C ) && isGrounded ) {
            isChop = true;
        }
        
        if( Input.GetKeyDown( KeyCode.C ) && !isGrounded ) {
            isJumpKick = true;  
        }
	}

    private void ResetInputVaule( ) {
        isMove = false;
        isJump = false;
        isJumpKick = false;
        isChop = false;
        isShoot = false;
    } 

    public void Dead( ) {
        ObjectManager.Instance.CreateObj( ENUM_Fx.DeathFx, transform.position );
        Destroy( this.gameObject );

        GameManager.Instance.GameOver( );
    } 
    	
	private void Jump( ) {
		
		isGrounded = false;

		m_Movement.Jump( m_Rb, m_JumpHeight );

		m_Anim.SetBool( "IsGrounded", isGrounded );

	}

    private void JumpKick( ) {

        int theDirX = isFacingRight ? 1 : -1;

        m_Rb.velocity = new Vector2(theDirX * m_JumpKickForce, -1 * m_JumpKickForce );

        m_Anim.SetTrigger( "JumpKick" );
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
    private IEnumerator SetInvincible( ) {
        isInvincible = true;
        int damageTimeCount = 20;

		while ( damageTimeCount >= 0 ) {
			
			gameObject.GetComponentInChildren<SpriteRenderer>( ).color = new Color( 1, 1, 1, 0 );
			
			yield return new WaitForSeconds( 0.05f );
			
			gameObject.GetComponentInChildren<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
			
			yield return new WaitForSeconds( 0.05f );
			damageTimeCount--;
		}
        if( damageTimeCount <= 0 ) {
            isInvincible = false;
        }
    }

    private void OnCollisionEnter2D( Collision2D _Other ){

        if( _Other.gameObject.layer == LayerMask.NameToLayer( "MovingGround" ) ) {
            transform.parent = _Other.transform;
            isOnMovingPlatform = true;
        }

    }
    private void OnCollisionExit2D( Collision2D _Other ) {
		
		if ( _Other.gameObject.layer == LayerMask.NameToLayer( "MovingGround" ) ) {
			transform.parent = null;
            isOnMovingPlatform = false;
		}

	}
}
