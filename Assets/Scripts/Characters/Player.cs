using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    
    private Rigidbody2D m_Rb;

    //Input Variables
    private float m_Horizontal;

    //JumpAttack Variables
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

    //Invincible State Variables
    private bool isInvincible;

    public bool IsInvincible {
        get { return isInvincible; }
        set { isInvincible = value; }
    }

    [SerializeField]
    private float m_InvincibleTime;

    private static Player instance;

    public static Player Instance {
        get {
            if( instance == null ) {
                instance = GameObject.FindObjectOfType<Player>( );
            }
            return instance;
        }
    }

    public bool OnGround {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    public bool IsJumpKick {
        get { return isJumpKick; }
        set { isJumpKick = value; }
    }

    public bool IsJump {
        get { return isJump; }
        set { isJump = value; }
    }

    public Rigidbody2D Rb {
        get { return m_Rb; }
        set { m_Rb = value; }
    }

    public override void Start( ) {

        base.Start( );
        
        m_Rb = gameObject.GetComponent<Rigidbody2D>( );

        isFacingRight = true;

    }

    private void Update( ) {

        HandleInput( );

        if( m_HealthCtrl.IsHurt && !m_HealthCtrl.IsDead( ) ) {
        StartCoroutine( SetInvincible( ) );
        }
        if( m_HealthCtrl.IsDead( ) ) {
           
        }
    }

    private void FixedUpdate( ) {

        m_Horizontal = Input.GetAxis( "Horizontal" );
        
        isGrounded = IsGrounded( );

        HandleMovement( m_Horizontal );
        
        Flip( );

        HandleLayers( );

    }

    private void HandleInput( ) {
        if( Input.GetKeyDown( KeyCode.C ) ) {
   
            Anim.SetTrigger( "Attack" );
        }
        if( Input.GetKeyDown( KeyCode.Space ) ) {
        
            Anim.SetTrigger( "Jump" );
        }
        if( Input.GetKeyDown( KeyCode.C ) && !isGrounded ) {
          
            Anim.SetTrigger( "Attack" );
        }
    }

    private void HandleMovement( float _Horizontal ) {

        if( !isAttack ) {
            Rb.velocity = new Vector2( _Horizontal * m_MovementSpeed, Rb.velocity.y );
        }
        if( isJump && m_Rb.velocity.y == 0 ) {

            Rb.AddForce( new Vector2( 0, m_JumpForce ) );
        }

        if( isAttack && m_Rb.velocity.y != 0 ) {
            m_Rb.velocity = new Vector2( transform.localScale.x * m_JumpKickForce, -m_JumpKickForce );
        }

        Anim.SetFloat( "Velocity", Mathf.Abs( m_Horizontal ) );
   
    }

    private void Flip( ) {

        if( m_Horizontal > 0 && !isFacingRight || m_Horizontal < 0 && isFacingRight ) {
            ChangeDirection( );
        }

    }

    private void HandleLayers( ) {

        int m_Weight = isGrounded ? 0 : 1;
       
        m_Anim.SetLayerWeight( 1, m_Weight );
        
    }

    private bool IsGrounded( ) {

        if( Rb.velocity.y <= 0 ) {

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

    private IEnumerator SetInvincible( ) {

        IsInvincible = true;

        int damageTimeCount = 20;

		while ( damageTimeCount >= 0 ) {
			
            foreach( SpriteRenderer theRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>( ) ) {
                theRenderer.color = new Color( 1, 1, 1, 0 );

                yield return new WaitForSeconds( 0.05f );

                theRenderer.color = new Color( 1, 1, 1, 1 );

                yield return new WaitForSeconds( 0.05f );

                damageTimeCount--;
            }
            if( damageTimeCount <= 0 ) {
                IsInvincible = false;
            }
		}
    }

    public override void Death( ) {
        base.Death( );
        m_Rb.simulated = false;
        m_Anim.SetTrigger( "Dead" );
    }
} 
