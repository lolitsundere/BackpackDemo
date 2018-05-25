using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBoxController : MonoBehaviour {

    private Transform top;
    private bool isOpen;
    private EquipmentManager.Equipment treasure;

    void Start()
    {
        top = transform.GetChild(0);
        treasure = EquipmentManager.GetRandomEquipment();
    }

    public void OpenBox(Animator ani)
    {
        if (!isOpen)
        {
            StartCoroutine("Open", ani);
        }
    }

    private IEnumerator Open(Animator ani)
    {
        yield return new WaitForSeconds(0.5f);
        while (top.rotation.eulerAngles.x > 300 || top.rotation.eulerAngles.x < 10)
        {
            top.rotation = Quaternion.Euler(top.rotation.eulerAngles.x - 2, top.rotation.eulerAngles.y, top.rotation.eulerAngles.z);
            yield return 0;
        }
        ani.SetBool("Get", true);
        GameObject.Find("HeadLabel").GetComponent<UILabel>().text = string.Format("从宝箱中获得了{0}!!", treasure.name);
        SlotManager.AddEquipment(treasure); 
        isOpen = true;
    }
}
