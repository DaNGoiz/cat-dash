using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColorValue : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Slider>().value = 0.08f;//����Ĭ��ֵΪ��ɫ
    }

    void Update()
    {
        GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().catColorValue = GetComponent<Slider>().value;
        //����������ֵ��ֵ��StatusManager�е���ɫ�������
    }
}
