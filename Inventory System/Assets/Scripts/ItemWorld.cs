using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour {

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) {
        Transform transform;
        switch (item.itemType)
        {
            default:
            case Item.ItemType.M4:
                transform = Instantiate(ItemAssets.Instance.M4Prefab, position, Quaternion.identity);
                break;
            case Item.ItemType.Shotgun:
                transform = Instantiate(ItemAssets.Instance.ShotgunPrefab, position, Quaternion.identity);
                break;
            case Item.ItemType.Pistol:
                transform = Instantiate(ItemAssets.Instance.PistolPrefab, position, Quaternion.identity);

                break;
            case Item.ItemType.M79_Grenade_Launcher:
                transform = Instantiate(ItemAssets.Instance.Grenade_LauncherPrefab, position, Quaternion.identity);
                break;
            case Item.ItemType.Rail_Gun:
                transform = Instantiate(ItemAssets.Instance.Rail_GunPrefab, position, Quaternion.identity);
                break;
            case Item.ItemType.Rocket_Launcher:
                transform = Instantiate(ItemAssets.Instance.Rocket_LauncherPrefab, position, Quaternion.identity);
                break;
        }
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition,Vector3 dropDir, Item item) {

        ItemWorld itemWorld = SpawnItemWorld(dropPosition + 2*dropDir +Vector3.up, item);
        itemWorld.GetComponent<Rigidbody>().AddForce(dropDir * 4, ForceMode.Impulse);
        return itemWorld;
    }


    public Item item;
    private Rigidbody rigidbody;

    private AudioSource audioSource;
    public AudioClip weaponPickUp;
    public AudioClip weaponDrop;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            audioSource.clip = weaponPickUp;
            audioSource.Play();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.clip = weaponDrop;
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (transform.position.y < -1.7)
            rigidbody.isKinematic = true;

    }
    public void SetItem(Item item) {
        this.item = item;
    }

    public Item GetItem() {
        return item;
    }

}
