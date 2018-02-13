using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecletIconController : MonoBehaviour {

    private Image m_Img;

    private Transform m_StartTrans;

    [SerializeField]
    private Vector3 m_Offset;

    [SerializeField]
    private int m_SeclectCount = 0;

    [SerializeField]
    private Button[ ] m_Btn;

	// Use this for initialization
	void Start( ) {
		m_Img = this.gameObject.GetComponent<Image>( );
  	}
	
	// Update is called once per frame
	void Update( ) {

        if( Input.GetKeyDown( KeyCode.DownArrow ) && m_SeclectCount != 2 ) {
            m_Img.gameObject.transform.position = transform.position - m_Offset;
            m_SeclectCount++;
        }
        if( Input.GetKeyDown( KeyCode.UpArrow ) && m_SeclectCount != 0 ) {
            m_Img.gameObject.transform.position = transform.position + m_Offset;
            m_SeclectCount--;
        }
        
        if( Input.GetKeyDown( KeyCode.X ) && m_SeclectCount == 0  ) {
            GameManager.Instance.GameResume( );
        }
        if( Input.GetKeyDown( KeyCode.X ) && m_SeclectCount == 1  ) {
            GameManager.Instance.GameResume( );
            GameManager.Instance.LoadSence( "Title" );
        }
        if( Input.GetKeyDown( KeyCode.X ) && m_SeclectCount == 2  ) {
            GameManager.Instance.QuitGame( );
        }
	}

}
