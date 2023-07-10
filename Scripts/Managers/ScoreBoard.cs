using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int score;
    public int _Score => score;
    /// <summary>
    /// �÷�
    /// </summary>
    /// <param name="score">���ε÷���</param>
    public void Score(int score)
    {
        this.score += score;
    }
    /// <summary>
    /// ���÷���
    /// </summary>
    public void ResetScore()
    {
        score = 0;
    }
    /// <summary>
    /// ���÷���
    /// </summary>
    /// <param name="score"></param>
    public void Set(int score)
    {
        this.score = score;
    }
}
