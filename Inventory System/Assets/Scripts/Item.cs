using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{


    public enum ItemType
    {
        M4,
        Shotgun,
        Pistol,
        M79_Grenade_Launcher,
        Rail_Gun,
        Rocket_Launcher,
    }

    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.M4:
                return ItemAssets.Instance.M4;
            case ItemType.Shotgun:
                return ItemAssets.Instance.Shotgun;
            case ItemType.Pistol:
                return ItemAssets.Instance.Pistol;
            case ItemType.M79_Grenade_Launcher:
                return ItemAssets.Instance.Grenade_Launcher;
            case ItemType.Rail_Gun:
                return ItemAssets.Instance.Rail_Gun;
            case ItemType.Rocket_Launcher:
                return ItemAssets.Instance.Rocket_Launcher;
        }
    }


}