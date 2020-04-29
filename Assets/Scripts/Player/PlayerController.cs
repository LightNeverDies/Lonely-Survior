using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{



    public Inventory inventory;
    public GameObject Hand;
    CharacterController controller;
    Animator animator;

    private InventoryItemCollection mCurrentItem = null;

    private InteractableItemBase mInteractItem = null;

    public hud Hud;

    float Speed = 10.0f;
    float RotationSpeed = 80.0f;
    float Gravity = 10.0f;
    private Vector3 _moveDir = Vector3.zero;

    private HealthBar mhealthBar;
    private HealthBar mfoodBar;
    private HealthBar mwaterBar;

    public int Health = 100;
    public int Food = 100;
    public int Water = 100;

    private int startHealth;
    private int startFood;
    private int startWater;

    public float HungerRate = 1.5f;
    public float WaterRate = 2.0f;
    public float HealthRate = 5.0f;



    void FixedUpdate()
    {
        if (!IsDead)
        {
            if (mCurrentItem != null && Input.GetKey(KeyCode.G))
            {
               DropCurrentItem();
                
            }
        }
    }

    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;


        mhealthBar = Hud.transform.Find("HealthBar").GetComponent<HealthBar>();
        mhealthBar.Min = 0;
        mhealthBar.Max = Health;
        startHealth = Health;
        mhealthBar.SetHealth(Health);

        mfoodBar = Hud.transform.Find("FoodBar").GetComponent<HealthBar>();
        mfoodBar.Min = 0;
        mfoodBar.Max = Food;
        startFood = Food;
        mfoodBar.SetFood(Food);

        mwaterBar = Hud.transform.Find("WaterBar").GetComponent<HealthBar>();
        mwaterBar.Min = 0;
        mwaterBar.Max = Water;
        startWater = Water;
        mwaterBar.SetWater(Water);

        InvokeRepeating("IncreaseHunger", 0, HungerRate);
        InvokeRepeating("IncreaseWater", 0, WaterRate);
        InvokeRepeating("IncreaseHealth", 0, HealthRate);

    }

    public void GuardianKill()
    {
        TakeDamage(100);
    }

    #region Player Statistic
    public bool IsDead
    {
        get
        {
            return Health == 0;
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;

        mhealthBar.SetHealth(Health);
        if (IsDead)
        {
            //Dead();
            // Might be problem soon.
        }
    }

    public void Dead()
    {
        animator.SetTrigger("death");
    }
    public void Rehab(int amount)
    {
        Health += amount;
        if (Health > startHealth)
        {
            Health = startHealth;
        }

        mhealthBar.SetHealth(Health);
    }

    public void Eat(int amount)
    {
        Food += amount;
        if (Food > startFood)
        {
            Food = startFood;
        }

        mfoodBar.SetFood(Food);
        if(Food > 0)
        {
            IncreaseHunger();
        }

    }

    public void Drink(int amount)
    {
        Water += amount;
        if (Water > startWater)
        {
            Water = startWater;
        }

        mwaterBar.SetWater(Water);
        if (Water > 0)
        {
            IncreaseWater();
        }
    }

    public void IncreaseHunger()
    {
        if (!IsDead)
        {
            Food--;
            if (Food < 0)
                Food = 0;

            mfoodBar.SetFood(Food);
        }

    }

    public void IncreaseWater()
    {
        if (!IsDead)
        {
            Water--;
            if (Water < 0)
                Water = 0;

            mwaterBar.SetWater(Water);
        }
        

    }
    public void IncreaseHealth()
    {
        if (Water == 0)
        {
            if (!IsDead)
            {
                Health--;
                mhealthBar.SetHealth(Health);
            }
            else
            {
                CancelInvoke("IncreaseWater");
            }
        }
        if (Food == 0)
        {
            if (!IsDead)
            {
                Health--;
                mhealthBar.SetHealth(Health);
            }
            else
            {
                CancelInvoke("IncreaseHunger");
            }
        }
        if(IsDead)
        {
           CancelInvoke("IncreaseHealth");
           Dead();
        }
    }


    #endregion



    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        if (!IsDead)
        {
            InventoryItemCollection item = e.Item;
            GameObject goItem = (item as MonoBehaviour).gameObject;
            goItem.SetActive(true);

            goItem.transform.parent = null;
            if (item == mCurrentItem)
                mCurrentItem = null;
        }
    }

    public void SetItemActive(InventoryItemCollection item , bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
       if (e.Item.ItemType != EItemType.Consumable)
       {
         if (mCurrentItem != null)
         {
            SetItemActive(mCurrentItem, false);
         }
            InventoryItemCollection item = e.Item;

            SetItemActive(item, true);

            mCurrentItem = e.Item;
       }
    }

    private bool mLockPickUp = false;

    private void DropCurrentItem()
    {
        mLockPickUp = true;
        InventoryItemCollection item = mCurrentItem;
        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
        animator.SetTrigger("drop");
        inventory.DropRemovedItem(mCurrentItem);
        
       
        Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
        if (rbItem != null)
        {
            rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
            if (mCurrentItem == null)
            {
                SetItemActive(mCurrentItem, false);
            }
            Invoke("DoDropItem", 0.25f);
            
        }
        
    }

    public void DoDropItem()
    {
        mLockPickUp = false;
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());
            mCurrentItem = null;
        }
        
    }

    


    // Update is called once per frame
    void Update()
    {

        if (!IsDead)
        {
            Movement();
            Gestures();
            Attack();
            if (mInteractItem != null && Input.GetKeyDown(KeyCode.F))
            {
                // Interact animation
                mInteractItem.OnInteractAnimation(animator);
                InteractWithItem();

            }
        }
        
    }

    private void Gestures()
    {
      if(controller.isGrounded)
        {
            if(Input.GetKey(KeyCode.H))
            {
                animator.SetBool("Gestures", true);        
            }
            if (Input.GetKeyUp(KeyCode.H))
            {
                animator.SetBool("Gestures", false);

            }
        }
    }
    private void Attack()
    {
        if(controller.isGrounded)
        {
            if (mCurrentItem != null && !EventSystem.current.IsPointerOverGameObject())
            {
                if (mCurrentItem.transform.parent != null && Input.GetMouseButtonDown(0) && mCurrentItem.ItemType == EItemType.Weapon)
                {
                    animator.SetTrigger("attack");
                }
            }
        }
    }



    
    private void Movement()
    {

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (v < 0)
                v = 0;

            transform.Rotate(0, h * RotationSpeed * Time.deltaTime, 0);
            bool move = (v > 0) || (h != 0);
            if (controller.isGrounded)
            {
                move = (v > 0) || (h != 0);
                Speed = 10.0f;

                animator.SetBool("run", move);

                _moveDir = Vector3.forward * v;

                _moveDir = transform.TransformDirection(_moveDir);
                _moveDir *= Speed;

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
                {
                    Speed = 25.0f;
                    move = (v > 0) || (h != 0);

                    _moveDir = Vector3.forward * v;

                    _moveDir = transform.TransformDirection(_moveDir);
                    _moveDir *= Speed;
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    animator.SetBool("run", false);
                    _moveDir = new Vector3(0, 0, 0);
                // Starting Idle animation
                }
            }
            _moveDir.y -= Gravity * Time.deltaTime;

            controller.Move(_moveDir * Time.deltaTime);
        
    }
    public void InteractWithItem()
    {
        if (mInteractItem != null)
        {
            mInteractItem.OnInteract();

            if (mInteractItem is InventoryItemCollection)
            {
                InventoryItemCollection inventoryItem = mInteractItem as InventoryItemCollection;
                inventory.AddItem(inventoryItem);
                inventoryItem.OnPickup();

                if (inventoryItem.UseItemAfterPickup)
                {
                    inventory.UseItem(inventoryItem);
                }
            }
        }

        Hud.CloseMessagePanel();

        mInteractItem = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsDead)
        {
            InventoryItemCollection item = other.GetComponent<InventoryItemCollection>();
            if (item != null)
            {
                mInteractItem = item;
                Hud.OpenMessagePanel("");
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsDead)
        {
            InventoryItemCollection item = other.GetComponent<InventoryItemCollection>();
            if (item != null)
            {
                Hud.CloseMessagePanel();
                mInteractItem = null;
            }
        }

    }




}
