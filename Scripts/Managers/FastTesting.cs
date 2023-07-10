using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTesting : MonoBehaviour
{
    StatusManager statusManager;

    void Start()
    {
        statusManager = GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>();
    }

    void Update()
    {
        if (statusManager.gameStatus == StatusManager.GameStatus.Playing)
        {
            //如果按下R，重置玩家位置
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.FindWithTag(TagName.player).transform.position = new Vector3(0, 0, 0);
            }
            //如果按下D，玩家死亡
            if (Input.GetKeyDown(KeyCode.D))
            {
                GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().SwitchStatus(StatusManager.Status.Dead);
            }
        }
    }
}
