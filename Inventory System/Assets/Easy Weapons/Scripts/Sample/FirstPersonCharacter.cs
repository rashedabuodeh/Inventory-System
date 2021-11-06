using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonCharacter : MonoBehaviour
{
    [SerializeField] private float runSpeed = 8f;                                       // The speed at which we want the character to move
    [SerializeField] private float strafeSpeed = 4f;                                    // The speed at which we want the character to be able to strafe
    [SerializeField] private float jumpPower = 5f;                                      // The power behind the characters jump. increase for higher jumps

    [SerializeField] private AdvancedSettings advanced = new AdvancedSettings();        // The container for the advanced settings ( done this way so that the advanced setting are exposed under a foldout
    [SerializeField] private bool lockCursor = true;

    [System.Serializable]
    public class AdvancedSettings                                                       // The advanced settings
    {
        public float gravityMultiplier = 1f;                                            // Changes the way gravity effect the player ( realistic gravity can look bad for jumping in game )
        public PhysicMaterial zeroFrictionMaterial;                                     // Material used for zero friction simulation
        public PhysicMaterial highFrictionMaterial;                                     // Material used for high friction ( can stop character sliding down slopes )
        public float groundStickyEffect = 5f;                                           // power of 'stick to ground' effect - prevents bumping down slopes.
    }

    private CapsuleCollider capsule;                                                    // The capsule collider for the first person character
    private const float jumpRayLength = 0.7f;                                           // The length of the ray used for testing against the ground when jumping
    public bool grounded { get; private set; }
    private Vector2 input;
    private IComparer rayHitComparer;

    public List<GameObject> Weapons;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject background;
    public static bool gameIsPaused;
    public WeaponSystem weaponSystem;
    public List<Item> itemList;
    private Inventory inventory;
    public AudioSource audioSource;


    void Awake()
    {
        // Set up a reference to the capsule collider.
        capsule = GetComponent<Collider>() as CapsuleCollider;
        grounded = true;
        rayHitComparer = new RayHitComparer();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

    }

    private Item item;

    private void OnCollisionEnter(Collision other)
    {
        ItemWorld itemWorld = other.gameObject.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            item = itemWorld.GetItem();
            inventory.AddItem(item);
            for (int i = 0; i < weaponSystem.weapons.Count; i++)
            {
                if (item.itemType == weaponSystem.weapons[i].GetComponent<ItemWorld>().item.itemType)
                {
                    weaponSystem.weapons[i].GetComponent<Weapon>().Reload();
                    return;
                }
            }
            audioSource.Play();
            switch (item.itemType)
            {
                default:
                case Item.ItemType.M4: 
                    weaponSystem.weapons.Add( Weapons[0] ) ;
                    break;
                case Item.ItemType.Shotgun:
                    weaponSystem.weapons.Add(Weapons[1]);
                    break;
                case Item.ItemType.Pistol:
                    weaponSystem.weapons.Add(Weapons[2]);
                    break;
                case Item.ItemType.M79_Grenade_Launcher:
                    weaponSystem.weapons.Add(Weapons[3]);
                    break;
                case Item.ItemType.Beam_Gun:
                    weaponSystem.weapons.Add(Weapons[4]);
                    break;
            }
        }
    }


    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Shotgun:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Shotgun, amount = 1 });
                break;
            case Item.ItemType.Pistol:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Pistol, amount = 1 });
                break;
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        itemList = inventory.itemList; // temp ************
        if (Input.GetMouseButtonUp(0) && !gameIsPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
            if (gameIsPaused)
            {
                pauseMenu.gameObject.SetActive(true);
                background.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.I) )
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
            if (gameIsPaused)
            {
                uiInventory.gameObject.SetActive(true);
                background.gameObject.SetActive(true);
            }
        }
        if (!gameIsPaused)
        {
            pauseMenu.gameObject.SetActive(false);
            uiInventory.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
        }
    }


    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            uiInventory.gameObject.SetActive(false);
        }
    }



    public void FixedUpdate()
    {
        float speed = runSpeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool jump = Input.GetButton("Jump");

        input = new Vector2(h, v);

        // normalize input if it exceeds 1 in combined length:
        if (input.sqrMagnitude > 1) input.Normalize();

        // Get a vector which is desired move as a world-relative direction, including speeds
        Vector3 desiredMove = transform.forward * input.y * speed + transform.right * input.x * strafeSpeed;

        // preserving current y velocity (for falling, gravity)
        float yv = GetComponent<Rigidbody>().velocity.y;

        // add jump power
        if (grounded && jump)
        {
            yv += jumpPower;
            grounded = false;
        }

        // Set the rigidbody's velocity according to the ground angle and desired move
        GetComponent<Rigidbody>().velocity = desiredMove + Vector3.up * yv;

        // Use low/high friction depending on whether we're moving or not
        if (desiredMove.magnitude > 0 || !grounded)
        {
            GetComponent<Collider>().material = advanced.zeroFrictionMaterial;
        }
        else
        {
            GetComponent<Collider>().material = advanced.highFrictionMaterial;
        }


        // Ground Check:

        // Create a ray that points down from the centre of the character.
        Ray ray = new Ray(transform.position, -transform.up);

        // Raycast slightly further than the capsule (as determined by jumpRayLength)
        RaycastHit[] hits = Physics.RaycastAll(ray, capsule.height * jumpRayLength);
        System.Array.Sort(hits, rayHitComparer);


        if (grounded || GetComponent<Rigidbody>().velocity.y < jumpPower * .5f)
        {
            // Default value if nothing is detected:
            grounded = false;
            // Check every collider hit by the ray
            for (int i = 0; i < hits.Length; i++)
            {
                // Check it's not a trigger
                if (!hits[i].collider.isTrigger)
                {
                    // The character is grounded, and we store the ground angle (calculated from the normal)
                    grounded = true;

                    // stick to surface - helps character stick to ground - specially when running down slopes
                    //if (rigidbody.velocity.y <= 0) {
                    GetComponent<Rigidbody>().position = Vector3.MoveTowards(GetComponent<Rigidbody>().position, hits[i].point + Vector3.up * capsule.height * .5f, Time.deltaTime * advanced.groundStickyEffect);
                    //}
                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
                    break;
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * capsule.height * jumpRayLength, grounded ? Color.green : Color.red);


        // add extra gravity
        GetComponent<Rigidbody>().AddForce(Physics.gravity * (advanced.gravityMultiplier - 1));
    }


    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //used for comparing distances
    class RayHitComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
        }
    }

}
