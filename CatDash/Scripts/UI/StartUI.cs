using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public GameObject NPC;
    public GameObject NPCSmile;


    public void OpenStartUI()
    {
        gameObject.SetActive(true);
        NPC.SetActive(true);
        NPCSmile.SetActive(false);
        GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().gameStatus = StatusManager.GameStatus.Start;
    }


    public void CloseStartUI()
    {
        StartCoroutine(FindNPC());
    }

    IEnumerator FindNPC()
    {   
        NPC.SetActive(false);
        NPCSmile.SetActive(true);
        Vector3 NPCSmileScale = NPCSmile.transform.localScale;
        NPCSmile.transform.localScale = new Vector3(NPCSmileScale.x * 1.1f, NPCSmileScale.y * 1.1f, NPCSmileScale.z * 1.1f);
        yield return new WaitForSeconds(0.05f);
        NPCSmile.transform.localScale = NPCSmileScale;

        //等待1秒
        yield return new WaitForSeconds(1.5f);
        
        GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().GameStart();
        
        gameObject.SetActive(false);
    }

}
