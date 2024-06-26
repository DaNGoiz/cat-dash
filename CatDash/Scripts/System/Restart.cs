using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public RankingListUI rankingListUI;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject go = GameObject.FindWithTag("Objects");
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Destroy(go.transform.GetChild(i).gameObject);
            }
            GameObject.Find("TargetPointGenerator").GetComponent<TargetPointGenerator>().GenerateTargetPoint();
            GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().GameStart();
            GameObject.FindWithTag(TagName.player).transform.position = Vector3.zero;
            GameObject.FindWithTag(TagName.gameOverUI)?.gameObject.SetActive(false);
            GameObject.FindWithTag(TagName.player).GetComponent<PlayerControl>().Revive();
            GameObject.FindWithTag(TagName.manager).GetComponent<ScoreBoard>().ResetScore();
            rankingListUI.GetComponent<RankingListUI>().ClearContent();
        }
    }
}
