using System.Collections;
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

    public static bool Dragging;
    public static BackpackGridScript DraggingItemScirpt;
    public static string DraggingItemSpriteName;

    private void Awake()
    {
        slot = GetComponent<UISprite>();
        item = transform.GetChild(0).transform.GetComponent<UISprite>();
        eventTrigger = transform.GetComponent<UIEventTrigger>();
        eventTrigger.onDragStart.Add(new EventDelegate(DragStart));
        eventTrigger.onDragEnd.Add(new EventDelegate(DragEnd));
        eventTrigger.onDrag.Add(new EventDelegate(OnDrag));
        camera = GameObject.Find("UICamera").GetComponent<Camera>();
        DraggingItemSpriteName = "Empty";
    }

    public void SetEquipment(EquipmentManager.Equipment _equipment)
    {
        equipment = _equipment;
        item.spriteName = equipment.sprite_Name;
    }

    public void Focus()
    {
        slot.spriteName = "BackpackUI2";
    }

    public void Unfocus()
    {
        slot.spriteName = "BackpackUI1";
    }

    public void DragStart()
    {
        if (item.spriteName != "Empty" && DraggingItemScirpt == null)
        {
            DraggingItemScirpt = this;
            DraggingItemSpriteName = item.spriteName;
            item.spriteName = "Empty";

            TempItem = Instantiate(Resources.Load("Item"), transform.parent.parent) as GameObject;
            TempItem.GetComponent<UISprite>().spriteName = DraggingItemSpriteName;
            TempItem.transform.position = item.transform.position;
            gap = camera.ScreenToWorldPoint(Input.mousePosition) - item.transform.position;
        }
    }

    public void OnDrag()
    {
        if (TempItem != null)
        {
            TempItem.transform.position = camera.ScreenToWorldPoint(Input.mousePosition) - gap;
        }
    }

    public void DragEnd()
    {
        Destroy(TempItem);
        TempItem = null;
        Ray camRay = camera.ScreenPointToRay(Input.mousePosition);
        UISprite ItemPointerOn = null;
        RaycastHit UIHit;
        if (Physics.Raycast(camRay, out UIHit, 100))
        {
            try
            { 
                ItemPointerOn = UIHit.collider.transform.GetComponent<BackpackGridScript>().item;
            }catch{}
        }

        if (DraggingItemScirpt != null && ItemPointerOn != null && ItemPointerOn != DraggingItemScirpt)
        {
            DraggingItemScirpt.item.spriteName = ItemPointerOn.spriteName;
            ItemPointerOn.spriteName = DraggingItemSpriteName;
            DraggingItemScirpt = null;
            DraggingItemSpriteName = "Empty";
            SlotManager.SwapEquipment(DraggingItemScirpt.equipment, equipment, gameObject);
        }
        else if (DraggingItemScirpt != null)
        {
            DraggingItemScirpt.item.spriteName = DraggingItemSpriteName;
            DraggingItemScirpt = null;
            DraggingItemSpriteName = "Empty";
        }
    }

}
