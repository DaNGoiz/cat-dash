using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColorValue : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Slider>().value = 0.08f;//设置默认值为橙色
    }

    void Update()
    {
        GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().catColorValue = GetComponent<Slider>().value;
        //将滑动条的值赋值给StatusManager中的颜色储存变量
    }
}
