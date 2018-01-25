using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_Fx {
    RocketExplosionFx,
    DeathFx
}

public enum ENUM_Weapon {
    Rocket
}

public abstract class IObject : MonoBehaviour {

    public abstract GameObject CreateObj( ENUM_Fx _Em_Fx, Vector3 _SpawnPosition );
    public abstract GameObject CreateObj( ENUM_Weapon _Em_Weapon, Vector3 _SpawnPosition, Quaternion _SpawnRotation );

}
