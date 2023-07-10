using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    public GameObject theFirstPot;
    Animator tipAnimator;
    void Update()
    {
        if (theFirstPot == null)
        { tipAnimator.SetBool("Tip", true); }
    }
}
