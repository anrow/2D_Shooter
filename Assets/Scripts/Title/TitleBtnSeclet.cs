using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtnSeclet : MonoBehaviour {

    [SerializeField]
    private Vector2 m_Offset;

    private Vector2 m_Pos;

    private int m_SecletCount = 0;

    [SerializeField]
    private RectTransform[ ] m_Rect;
    


    public int SecletCount {
        get { return m_SecletCount; }
    }

    private void Start( ) {
        this.gameObject.GetComponent<RectTransform>( ).anchoredPosition = m_Pos;
    }

    private void Update( ) {


        if( Input.GetKeyDown( KeyCode.UpArrow ) ) {
            
            m_SecletCount--;

        }
        if( Input.GetKeyDown( KeyCode.DownArrow ) ) {

            m_SecletCount++;
        }

        if( m_SecletCount > 2 ) {
            m_SecletCount = 0;
        }

        if( m_SecletCount < 0 ) {
            m_SecletCount = 2;
        }

        if( Input.GetKeyDown( KeyCode.X ) && m_SecletCount == 0 ) {
           SceneManager.LoadScene( "Main" );
        }

        if( Input.GetKeyDown( KeyCode.X ) && m_SecletCount == 1 ) {
            
        }

        if( Input.GetKeyDown( KeyCode.X ) && m_SecletCount == 2 ) {
            Application.Quit( );
        }
        
        this.gameObject.GetComponent<RectTransform>( ).anchorMax = m_Rect[ m_SecletCount ].anchorMax;
        this.gameObject.GetComponent<RectTransform>( ).anchorMin = m_Rect[ m_SecletCount ].anchorMin;

    }

}
