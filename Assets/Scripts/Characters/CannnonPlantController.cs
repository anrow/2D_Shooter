using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannnonPlantController : MonoBehaviour {
    
    [SerializeField]
    private int m_ShootRate;

    [SerializeField]
    private float m_ShootTime;

    [SerializeField]
    private Transform m_ShootPoint;

    private float m_NextShootTime;

    private Animator m_Anim;

    private void Start( ) {
        m_Anim = this.gameObject.GetComponentInChildren<Animator>( );
        m_NextShootTime = 0f;
    }

    private void OnTriggerStay2D( Collider2D _Other ) {
        if( _Other.tag == "Player" && m_NextShootTime <= Time.time ) {
            m_NextShootTime = Time.time + m_ShootTime;
            if( Random.Range( 0, 10 ) >= m_ShootRate ) {
                ObjectManager.Instance.CreateObj( ENUM_Weapon.Spore, m_ShootPoint.position, Quaternion.identity  );
                m_Anim.SetTrigger( "IsShoot" );
            }
        }
    }
}
