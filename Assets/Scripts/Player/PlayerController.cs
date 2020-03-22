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

    private IInvetoryItem mCurrentItem = null;


    private IInvetoryItem mItemToPickup = null;

    public hud Hud;

    float Speed = 10.0f;
    float RotationSpeed = 80.0f;
    float Gravity = 10.0f;
    private Vector3 _moveDir = Vector3.zero;

    private HealthBar mhealthBar;

    public int Health = 100;



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
        
    }
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
            animator.SetTrigger("death");
        }
    }




    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        IInvetoryItem item = e.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = null;

        if (item == mCurrentItem)
            mCurrentItem = null;
    }

    private void SetItemActive(IInvetoryItem item , bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if(mCurrentItem != null)
        {
            SetItemActive(mCurrentItem,false);
        }
        IInvetoryItem item = e.Item;
        /*        GameObject goItem = (item as MonoBehaviour).gameObject;
                goItem.SetActive(true);

                goItem.transform.parent = Hand.transform;*/
        SetItemActive(item, true);

        mCurrentItem = e.Item;
    }

    private void DropCurrentItem()
    {


        animator.SetTrigger("drop");

        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;

        inventory.DropRemovedItem(mCurrentItem);

        Rigidbody rbItem;
        //Throw animation
       
        rbItem = goItem.AddComponent<Rigidbody>();
        
        if (rbItem != null)
        {
            rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
            Invoke("DoDropItem", 0.25f);
        }
    }

    public void DoDropItem()
    {

        if(gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
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
            if (mItemToPickup != null && Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("tr_pickup");
                inventory.AddItem(mItemToPickup);
                mItemToPickup.onPickup();
                Hud.CloseMessagePanel();
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
            if (mCurrentItem != null && Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("attack");
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

                animator.SetBool("run", move);

                _moveDir = Vector3.forward * v;

                _moveDir = transform.TransformDirection(_moveDir);
                _moveDir *= Speed;

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
                {
                    Speed = 25.0f;
                    move = (v > 0) || (h != 0);

                    animator.SetBool("run", move);

                    _moveDir = Vector3.forward * v;

                    _moveDir = transform.TransformDirection(_moveDir);
                    _moveDir *= Speed;
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    animator.SetBool("run", false);
                    _moveDir = new Vector3(0, 0, 0);
                }
            }
            _moveDir.y -= Gravity * Time.deltaTime;

            controller.Move(_moveDir * Time.deltaTime);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        IInvetoryItem item = other.GetComponent<IInvetoryItem>();
        if (item != null)
        {
            mItemToPickup = item;
            Hud.OpenMessagePanel("");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        IInvetoryItem item = other.GetComponent<IInvetoryItem>();
        if (item != null)
        {
            Hud.CloseMessagePanel();
            mItemToPickup = null;
        }

    }




}
