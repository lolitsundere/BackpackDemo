using UnityEngine;
using System.Collections;
//
public class BuildHeadWord : MonoBehaviour
{

    public GameObject Head;      //头顶的点
    public static Camera UCamera;
    private float baseFomat;     //默认字与摄像机的距离
    private float currentFomat;  //当前相机的距离
    private float Scale;
    void Start()
    {
        UCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        //计算以下默认的距离
        baseFomat = Vector3.Distance(Head.transform.position, Camera.main.transform.position);
        Scale = 1 - transform.localScale.x;//默认缩放差值
        currentFomat = 0;
    }

    void Update()
    {
        if (baseFomat != currentFomat)
        {
            //保存当前相机到文字UI的距离
            currentFomat = Vector3.Distance(Head.transform.position, Camera.main.transform.position);
            float myscale = baseFomat / currentFomat - Scale;  //计算出缩放比例 
            transform.position = WorldToUI(Head.transform.position); //计算UI显示的位置
            transform.localScale = Vector3.one * myscale;           //缩放UI 
        }
    }

    /// <summary>
    /// 把3D点换算成NGUI屏幕上的2D点。
    /// </summary>
    public static Vector3 WorldToUI(Vector3 point)
    {
        Vector3 pt = Camera.main.WorldToScreenPoint(point);  //将世界坐标转换为视口坐标
        Vector3 ff = UCamera.ScreenToWorldPoint(pt);//将视口坐标转换为世界坐标
        ff.z = 0;
        return ff;
    }
}