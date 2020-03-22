using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemCollection : MonoBehaviour, IInvetoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }

    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }


    public virtual void OnUse()
    {
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

    public virtual void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
            gameObject.transform.eulerAngles = DropRotation;
        }
    }

    public virtual void DropItem()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(transform.position);

        if (Physics.Raycast(ray, out hit, 20))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
            gameObject.transform.eulerAngles = DropRotation;
        }
    }


    public virtual void onPickup()
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        gameObject.SetActive(false);
    }


    public Vector3 PickPosition;
    public Vector3 PickRotation;

    public Vector3 DropRotation;
}
