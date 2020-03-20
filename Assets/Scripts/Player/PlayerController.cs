using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Rigidbody rigidbody; // our player's rigidbody component
  //  float speed = 10.0f;
  //  float rotation = 0f;
  //  float rotSpeed = 80;
   // float gravity = 10;


    public Inventory inventory;
    public GameObject Hand;
   // Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator animator;

    private IInvetoryItem mCurrentItem = null;


    private IInvetoryItem mItemToPickup = null;

    public hud Hud;

    float Speed = 10.0f;
    float RotationSpeed = 80.0f;
    float Gravity = 10.0f;
    private Vector3 _moveDir = Vector3.zero;



    void FixedUpdate()
    {

        if (mCurrentItem != null && Input.GetKeyDown(KeyCode.G))
        {
            DropCurrentItem();
        }

    }

    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
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

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {

        IInvetoryItem item = e.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = Hand.transform;

        mCurrentItem = e.Item;
    }

    private void DropCurrentItem()
    {


        animator.SetTrigger("drop");

        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;

        inventory.RemoveItem(mCurrentItem);

        Rigidbody rbItem;
        //Throw animation
       
        rbItem = goItem.AddComponent<Rigidbody>();
        if (rbItem != null)
        {
            rbItem.isKinematic = true;
            rbItem.AddForce(transform.forward * Time.deltaTime, ForceMode.Impulse);

            Invoke("DoDropItem", 0.25f);
        }
    }

    public void DoDropItem()
    {
        if (mCurrentItem != null)
        {
            Destroy((this.mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());

            mCurrentItem = null;
        }
    }



    // Update is called once per frame
    void Update()
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
            if(Input.GetMouseButtonDown(0))
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
