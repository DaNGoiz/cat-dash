using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    /// <summary>
    /// TextMeshPro���������
    /// </summary>
    TMP_Text tmp;
    Timer timer;
    /// <summary>
    /// ��ʾ��ʱ��
    /// </summary>
    int timeDisplay;
    void Awake()
    {
        tmp = GetComponent<TMP_Text>();
        timer = GameObject.FindWithTag(TagName.manager).GetComponent<Timer>();
    }
    void Update()
    {
        GetTime();
        tmp.text = timeDisplay.ToString();
        transform.localScale = new Vector3(Mathf.PingPong(Time.time, 0.5f) + 1, Mathf.PingPong(Time.time, 0.5f) + 1, 1);
    }
    /// <summary>
    /// �Ӽ�ʱ������ȡʣ��ʱ��
    /// </summary>
    private void GetTime()
    {
        timeDisplay = timer.TimerCountInt;
    }
}
