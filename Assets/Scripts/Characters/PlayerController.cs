using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    private Rigidbody2D m_Rb;

    private Animator m_Anim;

    // Movement Variables
    [SerializeField]
    private float m_MaxSpeed = 0;

    [SerializeField]
    private bool isFacingRight = true;

    [SerializeField]
    private Transform m_GroundPointTrans;
    
    private bool isGrounded = false;
    
    private const float POINT_RADIUS = 0.2f;
    
    [SerializeField]
    private LayerMask groundLayer; 

    [SerializeField]
    private float m_JumpHeight;
    
    private void Start( ) {

        if( m_MaxSpeed <= 0 ) {
            m_MaxSpeed = 5;
        }

        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );

        m_Anim = this.gameObject.GetComponentInChildren<Animator>( );
    }

    private void Update( ) {
        if( Input.GetAxis( "Jump" ) > 0 && isGrounded ) {
            isGrounded = false;
            m_Anim.SetBool( "IsGrounded", isGrounded );
            m_Rb.AddForce( new Vector2( 0, m_JumpHeight ) );
        }
    }

    private void FixedUpdate( ) {

        isGrounded = Physics2D.OverlapCircle( m_GroundPointTrans.position, POINT_RADIUS, groundLayer );
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
