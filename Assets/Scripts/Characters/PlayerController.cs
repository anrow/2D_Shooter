using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    private Rigidbody2D m_Rb;

    private Animator m_Anim;

	private HealthController m_Health;

    // Movement Variables
    [SerializeField]
    private float m_MaxSpeed = 0;

    [SerializeField]
    private bool isFacingRight = true;

    //Jumping Variables
    [SerializeField]
    private Transform m_GroundPoint;
    
    private bool isGrounded = false;
    
    private const float POINT_RADIUS = 0.2f;
    
    [SerializeField]
    private LayerMask groundLayer; 

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
    }

    private void Update( ) {

        if( Input.GetKeyDown( KeyCode.Space ) && isGrounded ) {
            isGrounded = false;
            m_Anim.SetBool( "IsGrounded", isGrounded );
            m_Rb.AddForce( new Vector2( 0, m_JumpHeight ) );
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
			ObjectManager.Instance.CreateObj (ENUM_Fx.DeathFx, transform.position);
			Destroy (this.gameObject);
		}
			
    }

    private void FixedUpdate( ) {

        isGrounded = Physics2D.OverlapCircle( m_GroundPoint.position, POINT_RADIUS, groundLayer );
        m_Anim.SetBool( "IsGrounded", isGrounded );
        m_Anim.SetFloat( "JumpVelocity", m_Rb.velocity.y );

        float m_Horizontal = Input.GetAxis( "Horizontal" );

        m_Anim.SetFloat( "Velocity", Mathf.Abs( m_Horizontal ) );

        m_Rb.velocity = new Vector2( m_MaxSpeed * m_Horizontal, m_Rb.velocity.y );

        CheckFlip( m_Horizontal );

    }

    private void CheckFlip( float _AxisVaule ) {

		const int theFlipDirection = -1;

			if( _AxisVaule > 0 && !isFacingRight || _AxisVaule < 0 && isFacingRight ) {

				isFacingRight = !isFacingRight;

				Vector3 theScale = transform.localScale;

				theScale.x *= theFlipDirection;

				this.gameObject. transform.localScale = theScale;
			}
	}
}
