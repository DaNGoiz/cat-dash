using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingListUI : MonoBehaviour
{
    public RectTransform titleTransform;
    public RectTransform nameListTransform;
    public GameObject NamePrefab;
    private RankingList rankingList;
    public GameObject content;
    public Vector3 namePosition;
    public IEnumerator ShowTitle()
    {
        titleTransform.localPosition = new(400f, 380f, 0f);
        Vector3 des = new(0f, 380f, 0f);
        float sqrDistance = (titleTransform.localPosition - des).sqrMagnitude;
        TMP_Text text = titleTransform.GetComponent<TMP_Text>();
        text.color = new(1f, 0.9163379f, 0.5320755f, 0f);
        while ((titleTransform.localPosition - des).sqrMagnitude > 0.02f)
        {
            titleTransform.localPosition = Vector3.Lerp(titleTransform.localPosition, des, 0.1f);
            text.color = new(1f, 0.9163379f, 0.5320755f, 1 - ((titleTransform.localPosition - des).sqrMagnitude / sqrDistance));
            yield return null;
        }
        titleTransform.localPosition = des;
        text.color = new(1f, 0.9163379f, 0.5320755f, 1f);
    }
    public IEnumerator ShowNameList()
    {
        nameListTransform.localPosition = new(400f, 0f, 0f);
        Vector3 des = new(0f, 0f, 0f);
        Image image = nameListTransform.GetComponent<Image>();
        float sqrDistance = (nameListTransform.localPosition - des).sqrMagnitude;
        while ((nameListTransform.localPosition - des).sqrMagnitude > 0.02f)
        {
            nameListTransform.localPosition = Vector3.Lerp(nameListTransform.localPosition, des, 0.1f);
            image.color = new(1f, 1f, 1f, 1 - ((nameListTransform.localPosition - des).sqrMagnitude / sqrDistance));
            yield return null;
        }
        nameListTransform.localPosition = des;
        image.color = new(1f, 1f, 1f, 1f);
        ShowNames();
    }
    private void ShowNames()
    {
        Save.LoadRankingList();
        float dis = 50f;
        rankingList = GameObject.FindWithTag(TagName.manager).GetComponent<RankingList>();
        if (rankingList.list != null)
        {
            foreach (DictionaryEntry entry in rankingList.list.Cast<DictionaryEntry>())
            {
                GameObject userName = Instantiate(NamePrefab);
                userName.transform.SetParent(content.transform, false);
                userName.GetComponent<RectTransform>().localPosition = namePosition;
                userName.GetComponent<TMP_Text>().text = (string)entry.Key;
                userName.transform.GetChild(0).GetComponent<TMP_Text>().text = entry.Value.ToString();
                namePosition.y -= dis;
            }
        }
    }
    public void ClearContent()
    {
        namePosition = new Vector3(100f, -50f, 0f);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }
}
