using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeController : MonoBehaviour {
    
    private Rigidbody2D m_Rb;

    [SerializeField]
    private float m_MaxSpeed;

    [SerializeField]
    private float m_MinSpeed;

    [SerializeField]
    private float m_Angle;

    [SerializeField]
    private float m_Torque;

    private void Awake( ) {
        m_Rb = this.gameObject.GetComponent<Rigidbody2D>( );
        m_Rb.AddForce( new Vector2( Random.Range( -m_Angle, m_Angle ), Random.Range( m_MinSpeed, m_MaxSpeed ) ), ForceMode2D.Impulse );

        m_Rb.AddTorque( Random.Range( -m_Torque, m_Torque ) );
    }
}

