using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    private Rigidbody2D m_Rb;

    private Animator m_Anim;

    //Input Variables
    private float m_Horizontal;

    //Movement Variables
    [SerializeField]
    private float m_MovementSpeed;

    private bool isFacingRight;

    //Attack Variables
    private bool isPunch;

    private bool isJumpKick;

    [SerializeField]
    private float m_JumpKickForce;

    //Grounded Variables
    [SerializeField]
    private Transform[ ] m_GroundPoints;

    [SerializeField]
    private float m_PointRadius;

    [SerializeField]
    private LayerMask m_GroundLayer;

    private bool isGrounded;

    //Jump Variables
    private bool isJump;

    [SerializeField]
    private float m_JumpForce;

    private void Start( ) {

        m_Rb = gameObject.GetComponent<Rigidbody2D>( );

        m_Anim = gameObject.GetComponentInChildren<Animator>( );

        isFacingRight = true;

    }

    private void Update( ) {
        HandleInput( );
    }

    private void FixedUpdate( ) {

        m_Horizontal = Input.GetAxis( "Horizontal" );
        
        isGrounded = IsGrounded( );

        HandleMovement( m_Horizontal );
        
        Flip( );

        HandleAttacks( );

        HandleLayers( );

        ResetInputVaules( );
    }

    private void HandleInput( ) {
        if( Input.GetKeyDown( KeyCode.C ) ) {
            isPunch = true;
        }
        if( Input.GetKeyDown( KeyCode.Space ) ) {
            isJump = true;
        }
        if( Input.GetKeyDown( KeyCode.C ) && !isGrounded ) {
            isJumpKick = true;
        }
    }

    private void HandleMovement( float _Horizontal ) {

        if( !m_Anim.GetCurrentAnimatorStateInfo( 0 ).IsTag( "Attack" ) ) {

             m_Rb.velocity = new Vector2( _Horizontal * m_MovementSpeed, m_Rb.velocity.y );

        }

        if( isGrounded && isJump ) {
            isGrounded = false;
            m_Rb.AddForce( new Vector2( 0, m_JumpForce ) );
            m_Anim.SetTrigger( "JumpUp" );
        }

        m_Anim.SetFloat( "Velocity", Mathf.Abs( m_Horizontal ) );
        m_Anim.SetBool( "IsGrounded", m_Rb.velocity.y == 0 );
    }

    private void Flip( ) {

        const int m_DIR = -1;

        if( m_Horizontal > 0 && !isFacingRight || m_Horizontal < 0 && isFacingRight ) {

            isFacingRight = !isFacingRight;

            Vector3 theScale = this.gameObject.transform.localScale;

            theScale.x *= m_DIR;

            this.gameObject.transform.localScale = theScale;

        }

    }

    private void HandleAttacks( ) {

        if( isPunch && !m_Anim.GetCurrentAnimatorStateInfo( 0 ).IsTag( "Attack" ) ) {
            m_Anim.SetTrigger( "Chop" );
            m_Rb.velocity = Vector2.zero;
        }
        if( isJumpKick && !m_Anim.GetCurrentAnimatorStateInfo( 0 ).IsTag( "Attack" ) ) {
            m_Anim.SetTrigger( "JumpKick" );
            m_Rb.velocity = new Vector2( transform.lossyScale.x * m_JumpKickForce, -m_JumpKickForce );
            if( isGrounded ) {
                m_Rb.velocity = Vector2.zero;
            }
        }

    }

    private void HandleLayers( ) {

        int m_Weight = isGrounded ? 0 : 1;
       
        m_Anim.SetLayerWeight( 1, m_Weight );
        
    }

    private void ResetInputVaules( ) {
        isPunch = false;
        isJump = false;
        isJumpKick = false;
    }

    private bool IsGrounded( ) {

        if( m_Rb.velocity.y <= 0 ) {

            foreach( Transform _Point in m_GroundPoints ) {

                Collider2D[ ] theColliders = Physics2D.OverlapCircleAll( _Point.position, m_PointRadius, m_GroundLayer );

                for( int i = 0; i < theColliders.Length; i++ ) {
                    if( theColliders[ i ].gameObject != this.gameObject ) {
                        return true;
                    }
                }

            }

        }
        return false;
    }
} 
