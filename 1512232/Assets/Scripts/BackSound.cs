using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSound : MonoBehaviour
{
    public AudioSource Backsound;
    public void backsoundOnOff()
    {
        AudioSource bgsound = Backsound.GetComponent<AudioSource>();
        if (bgsound.mute==true)
        {
            bgsound.mute = false;
        }
        else
        {
            bgsound.mute = true;
        }
    }
}
