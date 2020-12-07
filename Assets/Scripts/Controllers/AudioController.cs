using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // music


    //sfx
    public AudioSource tony_stress;
    public AudioSource laura_stress;
    public AudioSource robert_stress;
    public AudioSource[] hitSounds;
    public AudioSource heal;
    public AudioSource buff;

    public void PlayPlayerStress(string name)
    {
        if (name.ToLower() == "tony")
        {
            tony_stress.Play();
        } 
        else if (name.ToLower() == "robert")
        {
            robert_stress.Play();
        }
        else if (name.ToLower() == "laura")
        {
            laura_stress.Play();
        }
    }


    public void PlayHit()
    {
        var index = (int) (Random.value * hitSounds.Length);
        hitSounds[index].Play();
    }


    public void PlayHeal()
    {
        heal.Play();
    }

    public void PlayBuff()
    {
        buff.Play();
    }
}
