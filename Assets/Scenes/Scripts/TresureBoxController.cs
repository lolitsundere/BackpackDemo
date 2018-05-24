using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBoxController : MonoBehaviour {

    private Transform top;
    private bool isOpen;

    void Start()
    {
        top = transform.GetChild(0);
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
        GameObject.Find("HeadLabel").GetComponent<UILabel>().text = "从宝箱中获得了XX!!";
        isOpen = true;
    }
}
