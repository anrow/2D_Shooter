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

    private void Start( ) {

        if( m_MaxSpeed <= 0 ) {
            m_MaxSpeed = 5;
        }

        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );

        m_Anim = this.gameObject.GetComponentInChildren<Animator>( );

		m_Health = this.gameObject.GetComponent<HealthController>( );

        m_Movement = new CharacterMovement( );
    }

    private void Update( ) {

        if( Input.GetKeyDown( KeyCode.Space ) && isGrounded ) {

            isGrounded = false;

            m_Anim.SetBool( "IsGrounded", isGrounded );

            m_Movement.Jump( m_Rb, m_JumpHeight );
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
			ObjectManager.Instance.CreateObj( ENUM_Fx.DeathFx, transform.position );
			Destroy (this.gameObject);
		}
			
    }

    private void FixedUpdate( ) {
        
        isGrounded = Physics2D.OverlapCircle( m_GroundPoint.position, POINT_RADIUS, m_GroundLayer );
        m_Anim.SetBool( "IsGrounded", isGrounded );

        m_Anim.SetFloat( "JumpVelocity", m_Rb.velocity.y );

        float m_Horizontal = Input.GetAxis( "Horizontal" );

        m_Anim.SetFloat( "Velocity", Mathf.Abs( m_Horizontal ) );
    
        m_Movement.Move( m_Rb, m_MaxSpeed, m_Horizontal );
        
        if( m_Horizontal > 0 && !isFacingRight || m_Horizontal < 0 && isFacingRight  ) {
            isFacingRight = !isFacingRight;
            m_Movement.Flip( this.gameObject );
        }


    }


}
