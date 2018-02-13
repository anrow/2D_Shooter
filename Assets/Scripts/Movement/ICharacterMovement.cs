using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterMovement {
    
    public abstract void Move( Rigidbody2D _Rb, float _Speed, float _Horizontal ); 
    public abstract void Move( Rigidbody2D _Rb, float _Speed ); 
    public abstract void Jump( Rigidbody2D _Rb, float _JumpForce );

    public abstract void Flip( GameObject _Go_Target );

    public abstract void MoveByPoint( Vector3 _StartPoint, Vector3 _EndPoint, float t );

}
