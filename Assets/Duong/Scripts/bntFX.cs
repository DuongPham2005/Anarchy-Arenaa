using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class bntFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public void HoverSound()
    {
        myFX.PlayOneShot (hoverFX);
    }
    public void ClickSound()
    {
        myFX.PlayOneShot(clickFX);
    }
}
