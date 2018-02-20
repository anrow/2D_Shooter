using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeState : IEnemyState {

    private Enemy m_Enemy;

    public void Enter( Enemy _Enemy ) {
        this.m_Enemy = _Enemy;
    }

    public void Execute( ) {
    }

    public void Exit( ) {
    }

    public void OnTriggerEnter2D( Collider2D _Otehr ) {
    }
}
