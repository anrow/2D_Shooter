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
	}
	
	// Update is called once per frame
	void FixedUpdate( ) {

		Vector3 targetCamPos = m_Target.position + m_Offset;

        transform.position = Vector3.Lerp( transform.position, targetCamPos, m_LerpRate * Time.deltaTime );

        if( transform.position.y < m_LowY ) {
            transform.position = new Vector3( transform.position.x, m_LowY, transform.position.z );
        }
	}
}
