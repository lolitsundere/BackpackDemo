using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontColliderScript : MonoBehaviour {

    internal GameObject TresureBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "TresureBox")
        {
            TresureBox = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(TresureBox))
        {
            TresureBox = null;
        }
    }
}
