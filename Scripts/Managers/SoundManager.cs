using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //（饼）有时间就写
    public AudioClip[] Brokening;

    public void PlayBrokening()
    {
        GetComponent<AudioSource>().clip = Brokening[0];
        GetComponent<AudioSource>().Play();
    }
}
