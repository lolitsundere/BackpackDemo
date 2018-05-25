using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackpackIconScript : MonoBehaviour
{
    TweenScale tweenScale;
    GameObject backpackContainer;
    private void Start()
    {
        tweenScale = transform.GetComponent<TweenScale>();
        backpackContainer = GameObject.Find("BackpackContainer");
        backpackContainer.SetActive(false);
    }
    
    public void PlayTween()
    {
        tweenScale.enabled = true;
    }

    public void StopTween()
    {
        tweenScale.enabled = false;
    }

    public void TurnBackpack()
    {
        backpackContainer.SetActive(!backpackContainer.activeSelf);
        SlotManager.SetEquipments();
    }
}
