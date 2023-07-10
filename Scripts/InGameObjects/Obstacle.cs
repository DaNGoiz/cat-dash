using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //如果被碰撞到，就显示结算UI栏
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().SwitchStatus(StatusManager.Status.Dead);
        }
    }
}
