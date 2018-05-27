using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    private Dictionary<int,EquipmentManager.Equipment> equipmentDic;
    private int nextItemIndex;
    public delegate int SortDelegate(EquipmentManager.Equipment e);

    public int Page;

    void Awake ()
    {
        equipmentDic = new Dictionary<int, EquipmentManager.Equipment>();
        nextItemIndex = 0;
        Page = 0;
    }

    public void AddEquipment(EquipmentManager.Equipment equipment)
    {
        equipmentDic.Add(nextItemIndex, equipment);
        SetNextItemIndex();
        SetEquipments();
    }

    public void RemoveEquipment(int i)
    {
        equipmentDic.Remove(i + Page * 24);
        SetNextItemIndex();
        SetEquipments();
    }

    public void SetEquipments()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (equipmentDic.ContainsKey(i + Page * 24))
            {
                transform.GetChild(i).GetComponent<BackpackGridScript>().SetEquipment(equipmentDic[i + Page * 24]);
            }
            else
            {
                transform.GetChild(i).GetComponent<BackpackGridScript>().SetEquipment(null);
            }
        }
    }

    private void SetNextItemIndex()
    {
        nextItemIndex = -1;
        while (equipmentDic.ContainsKey(++nextItemIndex)) ;
    }

    public void SwapEquipment(int i1, int i2)
    {
        if (equipmentDic.ContainsKey(i1 + Page * 24))
        {
            var temp = equipmentDic[i1 + Page * 24];
            equipmentDic[i1 + Page * 24] = equipmentDic[i2 + Page * 24];
            equipmentDic[i2 + Page * 24] = temp;
        }
        else
        {
            equipmentDic.Add(i1, equipmentDic[i2 + Page * 24]);
            equipmentDic.Remove(i2 + Page * 24);
            SetNextItemIndex();
        }
        SetEquipments();
    }

    public void Sort(SortDelegate d)
    {
        List<EquipmentManager.Equipment> list = new List<EquipmentManager.Equipment>();
        foreach (var item in equipmentDic)
        {
            item.Value.CompareItem = d(item.Value);
            list.Add(item.Value);
        }

        list.Sort();

        equipmentDic = new Dictionary<int, EquipmentManager.Equipment>();
        int i = 0;
        foreach (var item in list)
        {
            equipmentDic.Add(i++, item);
        }
        
        SetNextItemIndex();
        SetEquipments();
    }
}
