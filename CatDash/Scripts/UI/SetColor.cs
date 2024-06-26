using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour
{
    Color HSVColor;
    void Update()
    {
        HSVColor = Color.HSVToRGB(GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().catColorValue, 0.6f, 1);
        if (GetComponent<SpriteRenderer>() != null)
        { GetComponent<SpriteRenderer>().color = HSVColor; }
        else if (GetComponent<Image>() != null)
        { GetComponent<Image>().color = HSVColor; }
        //根据滑动条的值更改猫球的颜色
    }
}
