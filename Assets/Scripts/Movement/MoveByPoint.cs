using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPoint : MonoBehaviour {

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private Transform m_EndPoint;

    [SerializeField]
    private Transform[ ] points;

    [SerializeField]
    private int m_PointSeclet;

    private Transform m_StartPoint;
	// Use this for initialization
	void Start ( ) {

        m_EndPoint = points[ m_PointSeclet ];

        m_StartPoint = this.transform;
	}
	
	// Update is called once per frame
	void Update ( ) {

        this.transform.position = Vector3.MoveTowards( m_StartPoint.position, m_EndPoint.position, Time.deltaTime * m_Speed );

        if ( this.transform.position == m_EndPoint.position ) {

            m_PointSeclet++;

            if ( m_PointSeclet == points.Length ) {

                m_PointSeclet = 0;

            }

            m_EndPoint = points[ m_PointSeclet ];

        }

	}
}
