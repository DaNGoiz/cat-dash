using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleMoving : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time, 0.5f) + 1, Mathf.PingPong(Time.time, 0.5f) + 1, 1);
    }
}
