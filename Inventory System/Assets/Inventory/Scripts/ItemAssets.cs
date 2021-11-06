using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite M4;
    public Sprite Shotgun;
    public Sprite Pistol;
    public Sprite Grenade_Launcher;
    public Sprite Beam_Gun;

    public Sprite Rail_Gun;
    public Sprite Rocket_Launcher;
    public Sprite Bomb_Launcher;

}
