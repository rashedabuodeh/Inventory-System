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

    public Transform M4Prefab;
    public Transform ShotgunPrefab;
    public Transform PistolPrefab;
    public Transform Grenade_LauncherPrefab;
    public Transform Beam_GunPrefab;

    public Transform Rail_GunPrefab;
    public Transform Rocket_LauncherPrefab;
    public Transform Bomb_LauncherPrefab;

    public Sprite M4;
    public Sprite Shotgun;
    public Sprite Pistol;
    public Sprite Grenade_Launcher;
    public Sprite Beam_Gun;

    public Sprite Rail_Gun;
    public Sprite Rocket_Launcher;
    public Sprite Bomb_Launcher;

}
