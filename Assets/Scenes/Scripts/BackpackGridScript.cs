﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackGridScript : MonoBehaviour
{
    UISprite slot;
    UIEventTrigger eventTrigger;
    Camera camera;
    GameObject TempItem;
    Vector3 gap;
    EquipmentManager.Equipment equipment;
    UISprite item;
    GameObject BackpackIcon;

    private static GameObject DraggingItem;

    private void Awake()
    {
        slot = GetComponent<UISprite>();
        item = transform.GetChild(0).transform.GetComponent<UISprite>();
        eventTrigger = transform.GetComponent<UIEventTrigger>();
        eventTrigger.onDragStart.Add(new EventDelegate(DragStart));
        eventTrigger.onDragEnd.Add(new EventDelegate(DragEnd));
        eventTrigger.onDrag.Add(new EventDelegate(OnDrag));
        BackpackIcon = GameObject.Find("BackpackButton");
        camera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    public void SetEquipment(EquipmentManager.Equipment _equipment)
    {
        equipment = _equipment;
        if (equipment != null)
        {
            item.spriteName = equipment.sprite_Name;
        }
        else
        {
            item.spriteName = "ui_c_04";
        }
    }

    public void Focus()
    {
        slot.spriteName = "ui_y_01";
        if (equipment != null)
        {
            BackpackIcon.GetComponent<PackpackIconScript>().DescriptionLabel.transform.GetChild(0).GetComponent<UILabel>().text = equipment.ToString();
            BackpackIcon.GetComponent<PackpackIconScript>().DescriptionLabel.SetActive(true);
        }
    }

    public void Unfocus()
    {
        slot.spriteName = "ui_b_01";
        if (equipment != null)
        {
            BackpackIcon.GetComponent<PackpackIconScript>().DescriptionLabel.SetActive(false);
        }
    }

    public void DragStart()
    {
        if (item.spriteName != "ui_c_04" && DraggingItem == null)
        {
            DraggingItem = gameObject;
            if (TempItem == null)
            {
                TempItem = Instantiate(Resources.Load("Item"), transform.parent.parent) as GameObject;
            }
            else
            {
                TempItem.SetActive(true);
            }
            TempItem.GetComponent<UISprite>().spriteName = item.spriteName;
            TempItem.transform.position = item.transform.position;
            item.spriteName = "ui_c_04";
        }
    }

    public void OnPress()
    {
        gap = camera.ScreenToWorldPoint(Input.mousePosition) - item.transform.position;
    }

    public void OnDrag()
    {
        if (TempItem != null && TempItem.activeSelf)
        {
            TempItem.transform.position = camera.ScreenToWorldPoint(Input.mousePosition) - gap;
        }
    }

    public void DragEnd()
    {
        if (TempItem != null)
        {
            TempItem.SetActive(false);
            Ray camRay = camera.ScreenPointToRay(Input.mousePosition);
            UISprite ItemPointerOn = null;
            RaycastHit UIHit;
            if (Physics.Raycast(camRay, out UIHit, 100))
            {
                try
                {
                    if (UIHit.collider.transform.name == "TrashCan")
                    {
                        transform.parent.GetComponent<SlotManager>().RemoveEquipment(DraggingItem.transform.GetSiblingIndex());
                        DraggingItem = null;
                        return;
                    }
                    ItemPointerOn = UIHit.collider.transform.GetComponent<BackpackGridScript>().item;
                }
                catch { }
            }


            if (DraggingItem != null && ItemPointerOn != null && ItemPointerOn != DraggingItem)
            {
                transform.parent.GetComponent<SlotManager>().SwapEquipment(ItemPointerOn.parent.transform.GetSiblingIndex(), DraggingItem.transform.GetSiblingIndex());
            }
            else if (DraggingItem != null)
            {
                transform.parent.GetComponent<SlotManager>().SetEquipments();
            }
            DraggingItem = null;
        }
    }

}
