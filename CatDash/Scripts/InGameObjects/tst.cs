using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tst : MonoBehaviour
{
    void Start()
    {
        Debug.Log("StartforColl");     //输出调试
    }
    void Update()
    {
 
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name != "Ground")           //如果当前接触到的物体不是地面
            Debug.Log("与物体" + coll.gameObject.name + "发生碰撞");
    }
    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.name != "Ground")
            Debug.Log("与物体" + coll.gameObject.name + "重合");
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.name != "Ground")
            Debug.Log("离开物体" + coll.gameObject.name);
    }

}
