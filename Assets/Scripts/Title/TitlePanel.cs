using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour {

    [SerializeField]
    private Text m_TitleText;

    [SerializeField]
    private Image[ ] m_BtnsImg;


    private void Start( ) {

		m_TitleText.fontSize = 0;
        
        for( int i = 0; i < m_BtnsImg.Length; i++ ) {
              m_BtnsImg[ i ].color = Color.clear;
        }
    }
	void Update( ) {

        if( m_TitleText.fontSize != 110 ) {
            m_TitleText.fontSize++;
        }
        for( int i = 0; i < m_BtnsImg.Length; i++ ) {
            m_BtnsImg[ i ].color = Color.Lerp( Color.clear, new Color( 1, 1, 1, 1 ), Time.time * 0.4f );
        }
   
	}


}
