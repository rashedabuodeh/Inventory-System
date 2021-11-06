using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType
    {
        M4,
        Shotgun,
        Pistol,
        M79_Grenade_Launcher,
        Beam_Gun,
        Rail_Gun,
        Rocket_Launcher,
        Bomb_Launcher
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite() {
        switch (itemType) {
        default:
        case ItemType.M4:        
                return ItemAssets.Instance.M4;
        case ItemType.Shotgun: 
                return ItemAssets.Instance.Shotgun;
        case ItemType.Pistol:  
                return ItemAssets.Instance.Pistol;
        case ItemType.M79_Grenade_Launcher:        
                return ItemAssets.Instance.Grenade_Launcher;
        case ItemType.Beam_Gun:       
                return ItemAssets.Instance.Beam_Gun;
        }
    }

    //public Color GetColor() {
    //    switch (itemType) {
    //    default:
    //    case ItemType.M4:        return new Color(1, 1, 1);
    //    case ItemType.Shotgun: return new Color(1, 0, 0);
    //    case ItemType.Pistol:   return new Color(0, 0, 1);
    //    case ItemType.Coin:         return new Color(1, 1, 0);
    //    case ItemType.Medkit:       return new Color(1, 0, 1);
    //    }
    //}

    //public bool IsStackable() {
    //    switch (itemType) {
    //    default:
    //    case ItemType.Coin:
    //    case ItemType.Shotgun:
    //    case ItemType.Pistol:
    //        return true;
    //    case ItemType.M4:
    //    case ItemType.Medkit:
    //        return false;
    //    }
    //}

}
