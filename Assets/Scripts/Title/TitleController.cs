using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    [SerializeField]
    private PlayerTitleObjController m_Player;

    [SerializeField]
    private Text m_TitleText;

    [SerializeField]
    private Text m_GameInputText;

    [SerializeField]
    private Image[ ] m_BtnsImg;

    [SerializeField]
    private Text[ ] m_BtnsText;

	// Use this for initialization
	void Start( ) {

		m_TitleText.fontSize = 0;
        
        for( int i = 0; i < m_BtnsImg.Length; i++ ) {
              m_BtnsImg[ i ].color = Color.clear;
        }
        for( int i = 0; i < m_BtnsText.Length; i++ ) {
              m_BtnsText[ i ].color = Color.clear;
        }

        m_GameInputText.color = Color.clear;

	}
	
	// Update is called once per frame
	void Update( ) {
        if( m_TitleText.fontSize != 110 ) {
            m_TitleText.fontSize++;
        }
        for( int i = 0; i < m_BtnsImg.Length; i++ ) {
            m_BtnsImg[ i ].color = Color.Lerp( Color.clear, new Color( 1, 1, 1, 1 ), Time.time * 0.4f );
        }
        for( int i = 0; i < m_BtnsText.Length; i++ ) {
            m_BtnsText[ i ].color = Color.Lerp( Color.clear, new Color( 1, 1, 1, 1 ), Time.time * 0.4f );
        }
        if( m_Player.SecletCount == 0 ) {
            m_GameInputText.color = Color.Lerp( Color.clear, new Color( 1, 1, 1, 1 ), Time.time * 0.4f );
        } else {
            m_GameInputText.color = Color.Lerp( new Color( 1, 1, 1, 1 ), Color.clear, Time.time );
        }
	}
}
