using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameOverUI : MonoBehaviour
{
    RectTransform rectTransform;
    GameObject PlayerNameInputField;
    public RankingListUI RankingList;
    public TMP_Text tmp_text;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        PlayerNameInputField = GameObject.FindWithTag(TagName.playerNameInputField);
    }
    public void GameOver()
    {
        StartCoroutine(Display());
    }
    public IEnumerator Display()
    {
        RankingList.gameObject.SetActive(false);
        PlayerNameInputField.SetActive(false);
        Vector3 velocity = Vector3.zero;
        Vector3 des = new(0f, 150f, 0f);
        float springiness = 8f;
        float damping = 0.3f;
        bool flag = true;
        rectTransform.localPosition = new(0f, 500f, 0f);
        while (flag || (rectTransform.localPosition.y - des.y) <= 0)
        {
            if ((rectTransform.localPosition.y - des.y) <= 0) flag = false;
            Vector3 displacment = rectTransform.localPosition - des;
            velocity -= springiness * Time.deltaTime * displacment;  // 根据弹簧模型计算加速度
            rectTransform.localPosition += velocity * Time.deltaTime;  // 更新位置
            velocity *= 1.0f - damping * Time.deltaTime;  // 应用阻尼

            yield return null;
        }
        rectTransform.localPosition = des;
        StartCoroutine(ShowInputField());
    }
    public IEnumerator Close()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 des = new(0f, 500f, 0f);
        float springiness = 10f;
        float damping = 0.6f;
        while ((rectTransform.localPosition.y - des.y) <= 0)
        {
            Vector3 displacment = rectTransform.localPosition - des;
            velocity -= springiness * Time.deltaTime * displacment;
            rectTransform.localPosition += velocity * Time.deltaTime;
            velocity *= 1.0f - damping * Time.deltaTime;
            yield return null;
        }
        ShowRankingList();
        gameObject.SetActive(false);
    }
    private IEnumerator ShowInputField()
    {
        PlayerNameInputField.transform.localPosition = Vector3.zero;
        PlayerNameInputField.SetActive(true);
        PlayerNameInputField.GetComponent<TMP_InputField>().text = string.Empty;
        UnityEngine.UI.Image image = PlayerNameInputField.GetComponent<UnityEngine.UI.Image>();
        TMP_Text text = PlayerNameInputField.GetComponentInChildren<TMP_Text>();
        text.color = new Color(104f / 255f, 255f / 255f, 248f / 255f, 0f);
        //text.faceColor = new Color(104f, 255f, 248f, 1f);
        image.color = new Color(255f, 255f, 255f, 0f);
        while (image.color.a < 0.95)
        {
            image.color = new Color(255f, 255f, 255f, Mathf.Lerp(image.color.a, 1f, 0.005f));
            text.color = new Color(104f / 255f, 255f / 255f, 248f / 255f, Mathf.Lerp(text.color.a, 1f, 0.005f));
            yield return null;
        }
        image.color = new Color(255f, 255f, 255f, 1f);
        text.color = new Color(104f / 255f, 255f / 255f, 248f / 255f, 1f);
    }
    private void ShowRankingList()
    {
        RankingList.gameObject.SetActive(true);
        RankingList.StartCoroutine(RankingList.ShowTitle());
        RankingList.StartCoroutine(RankingList.ShowNameList());
    }
}
