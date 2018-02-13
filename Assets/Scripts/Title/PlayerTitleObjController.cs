using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTitleObjController : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D m_Rb;

    [SerializeField]
    private Animator m_Anim;
    
    [SerializeField]
    private Vector3 m_Offset;

    private int m_SecletCount = 0;

    public int SecletCount {
        get { return m_SecletCount; }
    }

    private void Update( ) {

        if( Input.GetKeyDown( KeyCode.UpArrow ) && m_SecletCount != 2 ) {
            this.transform.position = Vector3.Lerp( this.transform.position, this.transform.position + m_Offset, Time.time );
            m_SecletCount++;
        }
        if( Input.GetKeyDown( KeyCode.DownArrow ) && m_SecletCount != 0 ) {
            this.transform.position = Vector3.Lerp( this.transform.position, this.transform.position - m_Offset, Time.time );
            m_SecletCount--;
        }

        if( Input.GetKeyDown( KeyCode.X ) && m_SecletCount == 1 ) {
            Application.Quit( );
        }
        if( Input.GetKeyDown( KeyCode.X ) && m_SecletCount == 2 ) {
           SceneManager.LoadScene( "Main" );
        }

        m_Anim.SetBool( "IsGrounded", true );

    }

    private void OnTriggerEnter2D( Collider2D _Other ){
        if( _Other.gameObject.layer == LayerMask.NameToLayer( "Ground" ) ) {
            m_Rb.simulated = false;
        }
    }
}
