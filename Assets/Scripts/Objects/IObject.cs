using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_Fx {
    ExplosionFx,
    EnemyExplosionFx,
    DeathFx
}

public enum ENUM_Weapon {
    Rocket,
    Spore
}

public enum ENUM_Character {
    Player,
    EnemyBoar,
    CannonPlant
}

public enum ENUM_Item {
    Heart
}

public abstract class IObject : MonoBehaviour {

    public abstract GameObject CreateObj( ENUM_Fx _Em_Fx, Vector3 _SpawnPosition );
    public abstract GameObject CreateObj( ENUM_Weapon _Em_Weapon, Vector3 _SpawnPosition, Quaternion _SpawnRotation );
    public abstract GameObject CreateObj( ENUM_Character _Em_Character, Vector3 _SpawnPosition, Quaternion _SpawnRotation );

    public abstract GameObject CreateObj( ENUM_Item _Em_Item, Vector3 _SpawnPosition, Quaternion _SpawnRotation );
}
