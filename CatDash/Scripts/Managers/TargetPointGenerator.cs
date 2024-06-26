using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointGenerator : MonoBehaviour
{
    public GameObject[] targetPoint;
    public float cornerDistance = 0.5f;

    public void GenerateTargetPoint()
    {
        //获取屏幕范围
        float xMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float xMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float yMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float yMax = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        //在x和y范围内生成一个Vector2作为目标点，减去角落距离
        Vector2 targetPointPosition = new Vector2(Random.Range(xMin + cornerDistance, xMax - cornerDistance), Random.Range(yMin + cornerDistance, yMax - cornerDistance));

        //生成目标点
        GameObject tp = Instantiate(targetPoint[Random.Range(0, targetPoint.Length)], targetPointPosition, Quaternion.identity);
        tp.transform.SetParent(GameObject.FindWithTag("Objects").transform);
    }
}