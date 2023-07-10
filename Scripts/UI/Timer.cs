using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������¼�����Ҫ��ʱ���Լ�����
/// </summary>
public struct TimerEvent
{
    /// <summary>
    /// �Ƿ�ʹ��֡ʱ�䴥���¼�
    /// </summary>
    public bool useFrameTime;
    /// <summary>
    /// �������¼���ʱ����ֵ
    /// </summary>
    public float threshold;
    /// <summary>
    /// Ҫ�������¼�������Ϊ�޲�������
    /// </summary>
    public Action action;
}

public class Timer : MonoBehaviour
{
    /// <summary>
    /// ��ʱ��������״̬��Ĭ�ϳ�ʼδ��ʼ����
    /// </summary>
    public bool isRunning = false;
    public bool countdown = false;
    /// <summary>
    /// ��ʱ��ʲôʱ���Զ�ֹͣ
    /// </summary>
    public float autoStop = -1f;
    /// <summary>
    /// ��ǰ��ʱ��ʱ��(���������ʱ��Ϊ׼��Ĭ��)
    /// </summary>
    public float timerCount = 0f;
    /// <summary>
    /// ��ǰ��ʱ��ʱ��(���������ʱ��Ϊ׼��ȡ��)
    /// </summary>
    public int TimerCountInt => (int)timerCount;
    /// <summary>
    /// ��ǰ��ʱ��ʱ��(��֡ʱ��Ϊ׼)
    /// </summary>
    public float frameTimerCount = 0f;
    /// <summary>
    /// ��ǰ��ʱ��ʱ��(��֡ʱ��Ϊ׼��ȡ��)
    /// </summary>
    public int FrameTimerCountInt => (int)frameTimerCount;
    /// <summary>
    /// ��ʱ���¼��б���ʱ�����趨����ֵ��ᴥ����Ӧ���¼�
    /// </summary>
    public List<TimerEvent> events;
    void Start()
    {
        if (isRunning)
            StartTimer();
    }
    void Update()
    {
        if (isRunning)
        {
            frameTimerCount += countdown ? -Time.deltaTime : Time.deltaTime;
            CheckEvent();
        }
    }
    private void FixedUpdate()
    {
        if (isRunning)
        {
            timerCount += countdown ? -Time.fixedDeltaTime : Time.fixedDeltaTime;
            CheckEvent();
            if ((timerCount - autoStop > 0 && !countdown) || (timerCount - autoStop < 0 && countdown))
                StopTimer();
        }
    }
    /// <summary>
    /// ��ʼ��ʱ(����ʱ��δ������������ʱ)
    /// </summary>
    /// <param name="countdown">�Ƿ�Ϊ����ʱ</param>
    public void StartTimer()
    {
        isRunning = true;
    }
    /// <summary>
    /// ��ͣ��ʱ
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
    }
    /// <summary>
    /// ���ò�ֹͣ��ʱ��
    /// </summary>
    /// <param name="countdown">�Ƿ�Ϊ����ʱ</param>
    /// <param name="initValue">��ʱ��ʼֵ</param>
    /// <param name="autoStop">��ʱ��ʲôʱ���Զ�ֹͣ</param>
    public void SetTimer(bool countdown, float initValue, float autoStop = Mathf.Infinity)
    {
        timerCount = initValue;
        frameTimerCount = initValue;
        isRunning = false;
        this.countdown = countdown;
        this.autoStop = autoStop;
    }
    /// <summary>
    /// ���һ����ʱ���¼�
    /// </summary>
    /// <param name="threshold">�������¼���ʱ����ֵ</param>
    /// <param name="action">Ҫ�������¼�������Ϊ�޲�������</param>
    /// <param name="useFrameTime">�Ƿ�ʹ��֡ʱ�䴥���¼�</param>
    public void AddEvent(float threshold, Action action, bool useFrameTime = false)
    {
        TimerEvent ev = new()
        {
            useFrameTime = useFrameTime,
            threshold = threshold,
            action = action
        };
        events.Add(ev);
    }
    public void RemoveEvent(TimerEvent timerEvent)
    {
        events.Remove(timerEvent);
    }
    public void ClearEvent()
    {
        events.Clear();
    }
    private void CheckEvent()
    {
        if (events != null) 
            foreach (TimerEvent ev in events)
            {
                if (ev.useFrameTime)
                {
                    if (frameTimerCount >= ev.threshold)
                        ev.action?.Invoke();
                }
                else if (timerCount >= ev.threshold)
                    ev.action?.Invoke();
            }
    }
}
