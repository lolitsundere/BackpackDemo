using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBoxController : MonoBehaviour {

    private Transform top;

	void Start ()
    {
        top = transform.GetChild(0);	
	}
	
    public void OpenBox()
    {
        StartCoroutine("Open");
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(0.5f);
        while (top.rotation.eulerAngles.x > -60)
        {
            top.rotation = Quaternion.Euler(top.rotation.eulerAngles.x - 2, top.rotation.eulerAngles.y, top.rotation.eulerAngles.z);
            yield return 0;
        }
        yield return new WaitForSeconds(1f);

    }
}
