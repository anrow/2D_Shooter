using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float m_LerpRate;

    private Vector3 m_Offset;

    private float m_LowY;

	// Use this for initialization
	void Start( ) {
		
		m_Offset = transform.position - m_Target.position;

        m_LowY = transform.position.y;

        this.transform.position = m_Target.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate( ) {
		if( m_Target == null ) {
			return;
		}
		Vector3 targetCamPos = m_Target.position + m_Offset;

        transform.position = Vector3.Lerp( transform.position, targetCamPos, m_LerpRate * Time.deltaTime );

        if( transform.position.y < m_LowY ) {
            transform.position = new Vector3( transform.position.x, m_LowY, transform.position.z );
        }
        if( m_Target.GetComponent<PlayerController>( ).IsOnMovingPlatform( ) ) {
            m_LerpRate = 1;
        } else {
            m_LerpRate = 100;
        }
	}
}
