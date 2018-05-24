using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackpackIconScript : MonoBehaviour
{
    UIButton button;
    TweenScale tweenScale;
    private void Start()
    {
        button = transform.GetComponent<UIButton>();
        tweenScale = transform.GetComponent<TweenScale>();
        button.onClick.Add(new EventDelegate(()=>
        {
            tweenScale.enabled = true;
        }));
    }
    

}
