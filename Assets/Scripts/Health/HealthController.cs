using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    
    [SerializeField]
    private float m_MaxHealth;

	[SerializeField]
    private float m_CurrentHealth;

	public float CurrentHealth {
		get { return m_CurrentHealth; }
		set { m_CurrentHealth = value; }
	}

	public float MaxHealth {
		get { return m_MaxHealth; }
	}

    private bool isHurt;
    public bool IsHurt {
        get { return isHurt; }
        set { isHurt = value; }
    }

    public bool IsDead( ) {
		return m_CurrentHealth <= 0;
    }

    public void AddDamage( float _DamageVaule ) {
		if( _DamageVaule <= 0 ) {
			return;
		}
        isHurt = true;
        m_CurrentHealth -= _DamageVaule;
    }

    public void AddHealth( float _HealthVaule ) {

         m_CurrentHealth += _HealthVaule;

        if( m_CurrentHealth >= m_MaxHealth ) {
           m_CurrentHealth = m_MaxHealth;
        }
    }

    private void Start( ) {
        m_CurrentHealth = m_MaxHealth;
    }

}
