using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : IObject {
    
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

            case ENUM_Fx.ExplosionFx:
                theObj = Resources.Load( "Particles/ExplosionFx" ) as GameObject;
                break;
            case ENUM_Fx.EnemyExplosionFx:
                theObj = Resources.Load( "Particles/EnemyExplosionFx" ) as GameObject;
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

    public override GameObject CreateObj( ENUM_Character _Em_Character, Vector3 _SpawnPosition, Quaternion _SpawnRotation ) {

        GameObject theObj = null;

        switch( _Em_Character ) {
            case ENUM_Character.Player:
                theObj = Resources.Load( "Characters/Player" ) as GameObject;
                break;
            case ENUM_Character.EnemyBoar:
                theObj = Resources.Load( "Characters/EnemyBoar" ) as GameObject;
                break;
            case ENUM_Character.CannonPlant:
                theObj = Resources.Load( "Characters/CannonPlant" ) as GameObject;
                break;
        }

        Instantiate( theObj, _SpawnPosition, _SpawnRotation );
        
        return theObj;
    }

    public override GameObject CreateObj( ENUM_Item _Em_Item, Vector3 _SpawnPosition, Quaternion _SpawnRotation ) {

        GameObject theObj = null;

        switch( _Em_Item ) {
            case ENUM_Item.Heart:
                theObj = Resources.Load( "Items/Heart" ) as GameObject;
                break;
        }

        Instantiate( theObj, _SpawnPosition, _SpawnRotation );
        
        return theObj;
    }

}
