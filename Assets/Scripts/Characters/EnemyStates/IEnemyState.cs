using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {

    //Check the enemy state in this funtion
    void Execute( );

    void Enter( Enemy _Enemy );

    void Exit( );

    void OnTriggerEnter2D( Collider2D _Otehr );
}
