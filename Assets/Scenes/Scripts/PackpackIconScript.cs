using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackpackIconScript : MonoBehaviour
{
    TweenScale tweenScale;
    public GameObject BackpackContainer;
    public GameObject DescriptionLabel;
    private GameObject ButtonContainer;
    private GameObject TrashCan;
    private GameObject Left;
    private GameObject Right;
    private GameObject Page;


    private void Start()
    {
        tweenScale = transform.GetComponent<TweenScale>();
        BackpackContainer = GameObject.Find("BackpackContainer");
        ButtonContainer = GameObject.Find("ButtonContainer");
        TrashCan = GameObject.Find("TrashCan");
        Left = GameObject.Find("Left");
        Right = GameObject.Find("Right");
        Page = GameObject.Find("Page");
        DescriptionLabel = GameObject.Find("DescriptionLabel");
        Left.GetComponent<UIButton>().isEnabled = false;

        BackpackContainer.SetActive(false);
        ButtonContainer.SetActive(false);
        TrashCan.SetActive(false);
        Left.SetActive(false);
        Right.SetActive(false);
        Page.SetActive(false);
        DescriptionLabel.SetActive(false);
    }

    public void PlayTween()
    {
        tweenScale.enabled = true;
    }

    public void StopTween()
    {
        tweenScale.enabled = false;
    }

    public void PageUp()
    {
        int p = ++BackpackContainer.GetComponent<SlotManager>().Page;
        BackpackContainer.GetComponent<SlotManager>().SetEquipments();
        if (p == 2)
        {
            Right.GetComponent<UIButton>().isEnabled = false;
        }
        else
        {
            Left.GetComponent<UIButton>().isEnabled = true;
        }
        Page.GetComponent<UILabel>().text = p+1 + "/3";
    }

    public void PageDown()
    {
        int p = --BackpackContainer.GetComponent<SlotManager>().Page;
        BackpackContainer.GetComponent<SlotManager>().SetEquipments();
        if (p == 0)
        {
            Left.GetComponent<UIButton>().isEnabled = false;
        }
        else
        {
            Right.GetComponent<UIButton>().isEnabled = true;
        }
        Page.GetComponent<UILabel>().text = p+1 + "/3";
    }

    public void TurnBackpack()
    {
        BackpackContainer.SetActive(!BackpackContainer.activeSelf);
        ButtonContainer.SetActive(!ButtonContainer.activeSelf);
        TrashCan.SetActive(!TrashCan.activeSelf);
        Left.SetActive(!Left.activeSelf);
        Right.SetActive(!Right.activeSelf);
        Page.SetActive(!Page.activeSelf);
        BackpackContainer.GetComponent<SlotManager>().SetEquipments();
    }
}
