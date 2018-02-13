using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameClear : MonoBehaviour {
    private void OnTriggerEnter2D( Collider2D _Other ){
        if( _Other.gameObject.tag == "Player" ) {
            GameManager.Instance.GameClear( );
        }
    }
}
