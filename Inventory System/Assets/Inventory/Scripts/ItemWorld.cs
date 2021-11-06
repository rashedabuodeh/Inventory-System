using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{


    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
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
            case Item.ItemType.Beam_Gun:
                transform = Instantiate(ItemAssets.Instance.Beam_GunPrefab, position, Quaternion.identity);
                break;
        }

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + Vector3.one, item);
        itemWorld.GetComponent<Rigidbody>().AddForce(randomDir * 40f, ForceMode.Impulse);
        return itemWorld;
    }


    public Item item;
    //private SpriteRenderer spriteRenderer;
    //private UnityEngine.Experimental.Rendering.Universal.Light2D light2D;
    //private TextMeshPro textMeshPro;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        SetItem(item);
        //    light2D = transform.Find("Light").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        //    textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        //spriteRenderer.sprite = item.GetSprite();
        //light2D.color = item.GetColor();
        //if (item.amount > 1) {
        //    textMeshPro.SetText(item.amount.ToString());
        //} else {
        //    textMeshPro.SetText("");
        //}
    }

    public Item GetItem()
    {
        return item;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
    }


    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
