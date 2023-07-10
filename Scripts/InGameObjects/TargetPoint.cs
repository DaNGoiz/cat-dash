using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip BreakSound;
    [Header("Else")]
    public GameObject obstacle;
    public bool hasAnimation = false;

    Vector3 displayDirection;
    Quaternion rotation;

    void OnTriggerEnter2D(Collider2D collider)
    {   
        if (collider.gameObject.CompareTag("Player"))
        {
            // Change status to FallDown
            GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>().SwitchStatus(StatusManager.Status.FallDown);
            GenerateObstacle();
        }
    }

    void GenerateObstacle()
    {
        DirectionCalculation();
        if(hasAnimation){
            GetComponent<Animator>().SetTrigger("Destroy");
        }
        else{
            PlayBreakSound();
            DestroyObject();
        }
    }

    void DirectionCalculation()
    {
        //寻找player这个gameobject，获取它的移动方向向量
        Vector3 playerDirection = GameObject.Find("StatusManager").GetComponent<StatusManager>().playerPosition;
        //得到player位置和当前物体位置的差向量
        displayDirection = playerDirection - transform.position;
        //将display direction传到statusmanager的displayposition
        GameObject.Find("StatusManager").GetComponent<StatusManager>().displayPosition = displayDirection;
        //转化为quaternion
        rotation = Quaternion.FromToRotation(Vector3.up, - displayDirection);
        
        //将这个gameobject的rotation转到rotation
        transform.rotation = rotation;
        //上传rotation
        GameObject.Find("StatusManager").GetComponent<StatusManager>().displayDirection = Quaternion.FromToRotation(Vector3.up, displayDirection);
        
    }

    public void DestroyObject(){
        //生成对应障碍物
        GameObject go = Instantiate(obstacle, transform.position, rotation);
        go.transform.SetParent(GameObject.FindWithTag("Objects").transform);
        Destroy(gameObject);
    }

    public void PlayBreakSound(){
        GameObject.FindWithTag(TagName.audioSource).GetComponent<AudioSource>().PlayOneShot(BreakSound);
    }

}
