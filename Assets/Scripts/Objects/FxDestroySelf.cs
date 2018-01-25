using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDestroySelf : MonoBehaviour {

    [SerializeField]
    private float m_LifeTime;

    private void Awake( ) {
        Destroy( this.gameObject, m_LifeTime );
    }
}
