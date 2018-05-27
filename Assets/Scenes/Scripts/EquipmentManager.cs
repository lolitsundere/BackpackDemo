using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentArray EquipmentTypeArray;
	// Use this for initialization
	void Start ()
    {
        EquipmentTypeArray = JsonUtility.FromJson<EquipmentArray>(File.ReadAllText(Application.dataPath+"/Scenes/Json/Equipment.json"));
	}

    public static Equipment GetRandomEquipment()
    {
        int randomNum = new System.Random().Next(EquipmentTypeArray.items.Length);
        Equipment e = EquipmentTypeArray.items[randomNum];
        Equipment value = new Equipment();
        value.atk = e.atk;
        value.defense = e.defense;
        value.describe = e.describe;
        value.hp = e.hp;
        value.id = e.id;
        value.name = e.name;
        value.sprite_Name = e.sprite_Name;
        return value;
    }
	[Serializable]
    public class EquipmentArray
    {
        public Equipment[] items;
    }

    [Serializable]
	public class Equipment : IComparable
    {
        public int id;
        public int hp;
        public int defense;
        public int atk;
        public string name;
        public string describe;
        public string sprite_Name;

        public int CompareItem;

        public int CompareTo(object obj)
        {
            var e = obj as Equipment;
            return -CompareItem.CompareTo(e.CompareItem);
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1}\n生命 +{2}\n攻击 +{3}\n防御 +{4}",name,describe,hp,atk,defense);
        }
    }
}
