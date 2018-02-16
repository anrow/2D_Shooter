using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : ICharacterMovement {
        
    public override void Move( Rigidbody2D _Rb, float _Speed, float _Horizontal ) {
        _Rb.velocity = new Vector2( _Horizontal * _Speed, _Rb.velocity.y );
    }

    public override void Move( Rigidbody2D _Rb, float _Speed ){
         _Rb.velocity = new Vector2(  _Speed, _Rb.velocity.y );
    }

    public override void Jump( Rigidbody2D _Rb, float _JumpForce ) {
        _Rb.AddForce( new Vector2( 0, _JumpForce ) );
    }
    public override void Flip( GameObject _Go_Target ) {

		const int theFlipDirection = -1;

        Vector3 theScale = _Go_Target.transform.localScale;

		theScale.x *= theFlipDirection;

        _Go_Target.transform.localScale = theScale;
        
	}

    public override void MoveByPoint( Vector3 _StartPoint, Vector3 _EndPoint, float t ) { 

        _StartPoint = Vector3.MoveTowards( _StartPoint, _EndPoint, t );
        

    }
}

