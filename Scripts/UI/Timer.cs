using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 包含触发事件所需要的时间以及方法
/// </summary>
public struct TimerEvent
{
    /// <summary>
    /// 是否使用帧时间触发事件
    /// </summary>
    public bool useFrameTime;
    /// <summary>
    /// 触发该事件的时间阈值
    /// </summary>
    public float threshold;
    /// <summary>
    /// 要触发的事件，必须为无参数方法
    /// </summary>
    public Action action;
}

public class Timer : MonoBehaviour
{
    /// <summary>
    /// 计时器的运行状态，默认初始未开始运行
    /// </summary>
    public bool isRunning = false;
    public bool countdown = false;
    /// <summary>
    /// 计时到什么时候自动停止
    /// </summary>
    public float autoStop = -1f;
    /// <summary>
    /// 当前计时器时间(以物理更新时间为准，默认)
    /// </summary>
    public float timerCount = 0f;
    /// <summary>
    /// 当前计时器时间(以物理更新时间为准，取整)
    /// </summary>
    public int TimerCountInt => (int)timerCount;
    /// <summary>
    /// 当前计时器时间(以帧时间为准)
    /// </summary>
    public float frameTimerCount = 0f;
    /// <summary>
    /// 当前计时器时间(以帧时间为准，取整)
    /// </summary>
    public int FrameTimerCountInt => (int)frameTimerCount;
    /// <summary>
    /// 计时器事件列表，计时超过设定的阈值则会触发对应的事件
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
    /// 开始计时(若计时器未重置则会继续计时)
    /// </summary>
    /// <param name="countdown">是否为倒计时</param>
    public void StartTimer()
    {
        isRunning = true;
    }
    /// <summary>
    /// 暂停计时
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
    }
    /// <summary>
    /// 设置并停止计时器
    /// </summary>
    /// <param name="countdown">是否为倒计时</param>
    /// <param name="initValue">计时初始值</param>
    /// <param name="autoStop">计时到什么时候自动停止</param>
    public void SetTimer(bool countdown, float initValue, float autoStop = Mathf.Infinity)
    {
        timerCount = initValue;
        frameTimerCount = initValue;
        isRunning = false;
        this.countdown = countdown;
        this.autoStop = autoStop;
    }
    /// <summary>
    /// 添加一个计时器事件
    /// </summary>
    /// <param name="threshold">触发该事件的时间阈值</param>
    /// <param name="action">要触发的事件，必须为无参数方法</param>
    /// <param name="useFrameTime">是否使用帧时间触发事件</param>
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
