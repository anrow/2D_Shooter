using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : IObject {
    
    [SerializeField]
    private List<GameObject> m_FxList = new List<GameObject>( );

    private static ObjectManager instance;

    public static ObjectManager Instance {
        get {
            if( instance == null ) {
                instance = FindObjectOfType<ObjectManager>( );
            }
            return instance;
        }
    }

    public override GameObject CreateObj( ENUM_Fx _Em_Fx, Vector3 _SpawnPosition ) {

        GameObject theObj = null;

        switch( _Em_Fx ) {

            case ENUM_Fx.DeathFx:
                theObj = Resources.Load( "Particles/DeathFx" ) as GameObject;
                break;

            case ENUM_Fx.RocketExplosionFx:
                theObj = Resources.Load( "Particles/RocketExplosionFx" ) as GameObject;
                break;
        }

        Instantiate( theObj, _SpawnPosition, Quaternion.identity );
        
        return theObj;
    }
    public override GameObject CreateObj( ENUM_Weapon _Em_Weapon, Vector3 _SpawnPosition, Quaternion _SpawnRotation ) {

        GameObject theObj = null;

        switch( _Em_Weapon ) {
            case ENUM_Weapon.Rocket:
                theObj = Resources.Load( "Weapons/Rocket" ) as GameObject;
                break;
            case ENUM_Weapon.Spore:
                theObj = Resources.Load( "Weapons/Spore" ) as GameObject;
                break;
        }

        Instantiate( theObj, _SpawnPosition, _SpawnRotation );
        
        return theObj;
    }
}
