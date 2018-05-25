using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    private static Dictionary<EquipmentManager.Equipment, int> equipmentDic;
    private static HashSet<int> indexSet;
    private static int nextItemIndex;
    private static Transform t;
	// Use this for initialization
	void Start ()
    {
        equipmentDic = new Dictionary<EquipmentManager.Equipment, int>();
        indexSet = new HashSet<int>();
        nextItemIndex = 0;
        t = transform;
	}

    public static void AddEquipment(EquipmentManager.Equipment equipment)
    {
        equipmentDic.Add(equipment, nextItemIndex);
        indexSet.Add(nextItemIndex);
        while (indexSet.Contains(++nextItemIndex)) ;

    }

    public static void SetEquipments()
    {
        foreach (var item in equipmentDic)
        {
            t.GetChild(item.Value).GetComponent<BackpackGridScript>().SetEquipment(item.Key);
        }
    }

    public static void SwapEquipment(EquipmentManager.Equipment e1, EquipmentManager.Equipment e2, GameObject t2)
    {
        if (e2 == null)
        {
            int i = 0;
            while (!t.GetChild(i++).Equals(t2)) ;
            indexSet.Remove(equipmentDic[e1]);
            equipmentDic[e1] = i;
            indexSet.Add(i);
        }
        else
        {
            int temp = equipmentDic[e1];
            equipmentDic[e1] = equipmentDic[e2];
            equipmentDic[e2] = temp;
        }
    }
}
