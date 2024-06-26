using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int score;
    public int _Score => score;
    /// <summary>
    /// 得分
    /// </summary>
    /// <param name="score">本次得分数</param>
    public void Score(int score)
    {
        this.score += score;
    }
    /// <summary>
    /// 重置分数
    /// </summary>
    public void ResetScore()
    {
        score = 0;
    }
    /// <summary>
    /// 设置分数
    /// </summary>
    /// <param name="score"></param>
    public void Set(int score)
    {
        this.score = score;
    }
}
