using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using UnityEngine;

public class Save
{
    public int score;
    /// <summary>
    /// 保存排行榜
    /// </summary>
    public static void SaveRankingList(string userName, int score)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }
        if (!File.Exists(Application.persistentDataPath + "/Saves/_RankingList"))
        {
            using (File.Create(Application.persistentDataPath + "/Saves/_RankingList")) { }
        }
        RankingList rankingList = GameObject.FindWithTag(TagName.manager).GetComponent<RankingList>();
        rankingList.Add(userName, score);

        string jsonStr = JsonConvert.SerializeObject(rankingList.list);
        using StreamWriter writer = new(Application.persistentDataPath + "/Saves/_RankingList");
        writer.Write(jsonStr);
    }
    /// <summary>
    /// 读取排行榜
    /// </summary>
    public static void LoadRankingList()
    {
        if (File.Exists(Application.persistentDataPath + "/Saves/_RankingList"))
        {
            string listStr = File.ReadAllText(Application.persistentDataPath + "/Saves/_RankingList");
            OrderedDictionary list = JsonConvert.DeserializeObject<OrderedDictionary>(listStr);
            GameObject.FindWithTag(TagName.manager).GetComponent<RankingList>().list = list;
        }
        else GameObject.FindWithTag(TagName.manager).GetComponent<RankingList>().list = new();
    }
}
