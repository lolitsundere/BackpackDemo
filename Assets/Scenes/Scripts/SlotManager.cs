using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    private Dictionary<int,EquipmentManager.Equipment> equipmentDic;
    private EquipmentManager.Equipment[] equipmentArr;
    private int nextItemIndex;
    public delegate int SortDelegate(EquipmentManager.Equipment e);
    public GameObject EquipmentGrid;
    public GameObject StatesInfo;
    public int Page;

    void Awake ()
    {
        equipmentDic = new Dictionary<int, EquipmentManager.Equipment>();
        equipmentArr = new EquipmentManager.Equipment[7];
        nextItemIndex = 0;
        Page = 0;
        StatesInfo.GetComponent<UILabel>().text = string.Format("HP:{0}   Atk:{1}   Def:{2}", 0, 0, 0);
    }

    public void EquipEquipment(int i1, int i2)
    {
        if (equipmentDic[i2].sprite_Name.StartsWith(GetEquipmentType(i1)))
        {
            if(equipmentArr[i1] == null)
            {
                equipmentArr[i1] = equipmentDic[i2];
                RemoveEquipment(i2);
                SetNextItemIndex();
            }
            else
            {
                var temp = equipmentDic[i2];
                equipmentDic[i2] = equipmentArr[i1];
                equipmentArr[i1] = temp;
            }
        }
        SetEquipments();
    }

    public void UnequipEquipment(int i1, int i2)
    {
        if (equipmentDic.ContainsKey(i1))
        {
            if (equipmentDic[i1].sprite_Name.StartsWith(GetEquipmentType(i2)))
            {
                var temp = equipmentDic[i1];
                equipmentDic[i1] = equipmentArr[i2];
                equipmentArr[i2] = temp;
            }
        }
        else
        {
            equipmentDic.Add(i1, equipmentArr[i2]);
            equipmentArr[i2] = null;
        }
        SetEquipments();
    }

    public void Equip(int i)
    {
        if (equipmentDic.ContainsKey(i))
        {
            EquipEquipment(GetEquipmentIndex(equipmentDic[i].sprite_Name),i);
        }
    }

    public void Unequip(int i)
    {
        if (equipmentArr[i] != null)
        {
            UnequipEquipment(nextItemIndex, i);
            SetNextItemIndex();
        }
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

        int hp = 0;
        int atk = 0;
        int def = 0;
        for (int i = 0; i < equipmentArr.Length; i++)
        {
            if (equipmentArr[i] != null)
            {
                EquipmentGrid.transform.GetChild(i).GetComponent<BackpackGridScript>().SetEquipment(equipmentArr[i]);
                hp += equipmentArr[i].hp;
                atk += equipmentArr[i].atk;
                def += equipmentArr[i].defense;
            }
            else
            {
                EquipmentGrid.transform.GetChild(i).GetComponent<BackpackGridScript>().SetEquipment(null);
            }
        }

        StatesInfo.GetComponent<UILabel>().text = string.Format("HP:{0}   Atk:{1}   Def:{2}", hp, atk, def);

    }

    private void SetNextItemIndex()
    {
        nextItemIndex = -1;
        while (equipmentDic.ContainsKey(++nextItemIndex)) ;
    }

    private string GetEquipmentType(int i)
    {
        switch (i)
        {
            case 0:
                return "Hat";
            case 1:
                return "Ring";
            case 2:
                return "Cloth";
            case 3:
                return "Cuff";
            case 4:
                return "Belt";
            case 5:
                return "Weapon";
            case 6:
                return "Shose";
            default:
                return "";
        }
    }

    private int GetEquipmentIndex(string s)
    {
        if (s.StartsWith("Hat"))
        {
            return 0;
        }
        else if (s.StartsWith("Ring"))
        {
            return 1;
        }
        else if (s.StartsWith("Cloth"))
        {
            return 2;
        }
        else if (s.StartsWith("Cuff"))
        {
            return 3;
        }
        else if (s.StartsWith("Belt"))
        {
            return 4;
        }
        else if (s.StartsWith("Weapon"))
        {
            return 5;
        }
        else if (s.StartsWith("Shose"))
        {
            return 6;
        }
        else
        {
            return -1;
        }
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
