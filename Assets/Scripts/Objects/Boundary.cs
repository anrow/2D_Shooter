using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {
    
    void OnTriggerEnter2D( Collider2D _Other ) {
        if( _Other.tag == "Player" ) {
            _Other.gameObject.GetComponent<Player>( ).Death( );
        } 
    }
    
}
