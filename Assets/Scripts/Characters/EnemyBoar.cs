using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoar : MonoBehaviour {

    [SerializeField]
    private float m_Speed;

    //facing
    [SerializeField]
    private GameObject mGo_EnemyGraphic;

    private bool canFlip;

    private bool isFacingRight;

    [SerializeField]
    private float m_FlipTime;

    private float m_NextFlipTime;

    //Attacking
    [SerializeField]
    private float m_ChargeTime;

    private float m_StartChargeTime;

    private bool isChasing;

    private Rigidbody2D m_Rb;

    private Animator m_Anim;

    private CharacterMovement m_Movement;

	void Start( ) {
		m_Anim = this.gameObject.GetComponentInChildren<Animator>( );
        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );
        m_Movement = new CharacterMovement( );
	}
	
	void Update () {
		if( Time.time > m_NextFlipTime ) {
            if( Random.Range( 0, 10 ) >= 5 ) {
                Flip( );
                m_NextFlipTime = Time.time + m_FlipTime;
            }
        }
	}

    private void Flip( ) {
        if( !canFlip ) {
            return;
        }
        float facingX = mGo_EnemyGraphic.transform.localScale.x;
        facingX *= -1;
        mGo_EnemyGraphic.transform.localScale = new Vector3( facingX, mGo_EnemyGraphic.transform.localScale.y, mGo_EnemyGraphic.transform.localScale.z );
        isFacingRight = !isFacingRight;
    }

    private void OnTriggerEnter2D( Collider2D _Other ) {
        
        if( _Other.tag == "Player" ) {
            if( isFacingRight && _Other.transform.position.x < transform.position.x ) {
                Flip( );
            } else if( !isFacingRight && _Other.transform.position.x > transform.position.x ) {
                Flip( );
            }
            canFlip = false;
            isChasing = true;
            m_StartChargeTime = Time.time + m_ChargeTime;
        }
    }

    private void OnTriggerStay2D( Collider2D _Other ){
        
        if( _Other.tag == "Player" ) {
            if( m_StartChargeTime >= Time.time ) {
                if( !isFacingRight ) {
                    m_Rb.AddForce( Vector2.left * m_Speed );
                } else {
                    m_Rb.AddForce( Vector2.right * m_Speed );
                }
                m_Anim.SetBool( "IsChasing", isChasing );
            }
        }
    }

    private void OnTriggerExit2D( Collider2D _Other ) {
        if( _Other.tag == "Player" ) {
            canFlip = true;
            isChasing = false;
            m_Rb.velocity = Vector2.zero;
            m_Anim.SetBool( "IsChasing", isChasing );
        }
    }
}
