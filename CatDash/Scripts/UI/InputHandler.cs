using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnNameEntered(string name)
    {
        ScoreBoard scoreBoard = GameObject.FindWithTag(TagName.manager).GetComponent<ScoreBoard>();
        Save.SaveRankingList(name, scoreBoard._Score);
        StartCoroutine(GameObject.FindWithTag(TagName.gameOverUI).GetComponentInChildren<GameOverUI>().Close());
        StartCoroutine(Close());
    }
    public IEnumerator Close()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 des = new(0f, -600f, 0f);
        float springiness = 10f;
        float damping = 0.6f;
        while ((rectTransform.localPosition.y - des.y) >= 0)
        {
            Vector3 displacment = rectTransform.localPosition - des;
            velocity -= springiness * Time.deltaTime * displacment;
            rectTransform.localPosition += velocity * Time.deltaTime;
            velocity *= 1.0f - damping * Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}