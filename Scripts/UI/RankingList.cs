using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RankingList : MonoBehaviour
{
    public OrderedDictionary list = new();
    /// <summary>
    /// Add new user
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="score"></param>
    public void Add(string userName, int score)
    {
        if (list.Count > 0)
        {
            if (list.Contains(userName))
            {
                list[userName] = score;
            }
            else
            {
                bool flag = false;
                DictionaryEntry entry;
                for (int i = 0; i < list.Count; i++)
                {
                    entry = list.Cast<DictionaryEntry>().ElementAt(i);
                    if ((int)entry.Value.ConvertTo(typeof(int)) <= score)
                    {
                        list.Insert(i, userName, score);
                        flag = true;
                        break;
                    }
                }
                if(!flag) { list.Add(userName, score); }
            }
        }
        else list.Add(userName, score);
    }
    public void Remove(string userName)
    {
        list.Remove(userName);
    }
    public void Clear()
    {
        list.Clear();
    }
}
