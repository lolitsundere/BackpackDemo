using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContainerScript : MonoBehaviour
{
    public SlotManager slotManager;

    public void SortByHp()
    {
        slotManager.Sort(e => e.hp);
    }

    public void SortByAtk()
    {
        slotManager.Sort(e => e.atk);
    }

    public void SortByDef()
    {
        slotManager.Sort(e => e.defense);
    }
}
