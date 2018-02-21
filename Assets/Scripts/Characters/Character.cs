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

    protected Animator m_Anim;

    public Animator Anim {
        get { return m_Anim; }
        private set { m_Anim = value; }
    }

    public virtual void Start( ) {
        m_Anim = GetComponentInChildren<Animator>( );
    }

    public bool IsAttack {
        get { return isAttack; }
        set { isAttack = value; }
    }

    public void ChangeDirection( ) {
        const int THE_DIR = -1;
        isFacingRight = !isFacingRight;

        Vector3 theScale = this.gameObject.transform.localScale;

        theScale.x *= THE_DIR;

        this.gameObject.transform.localScale = theScale;
    }

}
