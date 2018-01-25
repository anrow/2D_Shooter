using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    
    [SerializeField]
    private float m_MaxHealth;

    private float m_CurrentHealth;

    private bool isDead = false;

    public bool IsDead( ) {
        return isDead;
    }

    public void Start( ) {
        m_CurrentHealth = m_MaxHealth;
    }

    public void AddDamage( float _Damage ) {

        if( _Damage <= 0 ) {
            return;
        }

        m_CurrentHealth -= _Damage;

        if( m_CurrentHealth <= 0 ) {
           isDead = true;
        }

    }
}
