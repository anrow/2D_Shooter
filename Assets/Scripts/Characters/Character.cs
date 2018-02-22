using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    //Movement Variables
    [SerializeField]
    protected float m_MovementSpeed;

    [SerializeField]
    protected bool isFacingRight;

    //Attack Variables
    protected bool isAttack;

    public bool IsAttack {
        get { return isAttack; }
        set { isAttack = value; }
    }


    //Animate
    protected Animator m_Anim;

    public Animator Anim {
        get { return m_Anim; }
        private set { m_Anim = value; }
    }

    //HealthControll
    protected HealthController m_HealthCtrl;

    public virtual void Start( ) {

        m_Anim = GetComponentInChildren<Animator>( );

        m_HealthCtrl = GetComponent<HealthController>( );

    }


    public void ChangeDirection( ) {
        const int THE_DIR = -1;
        isFacingRight = !isFacingRight;

        Vector3 theScale = this.gameObject.transform.localScale;

        theScale.x *= THE_DIR;

        this.gameObject.transform.localScale = theScale;
    }

    public virtual void Death( ) {
        Destroy( this.gameObject, 1f );
    }

}
