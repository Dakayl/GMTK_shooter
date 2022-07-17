using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoundPlayer : MonoBehaviour
{
    //static instance of the Player to check if we should keep or delete new instances
    protected static SoundPlayer Instance;
    protected static float globalVolume = 1.0f;
    protected static List<AudioSource> soundSource;


    protected static int soundPlayed = 0;
    protected void Start()
    {       
             
        if (SoundPlayer.Instance == null)
        {
            soundSource = new List<AudioSource>();
            for(int index = 0; index < 6; index++) {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.loop = false;
                source.volume = RangeVolume(globalVolume);
                soundSource.Add(source);
            }
            
     
            //save a reference to this instance in the static variable
            SoundPlayer.Instance = this;
        }
        else
        {
            //Only one instance
            Destroy(this.gameObject);
        }
    }

    private static float RangeVolume(float volume) {
        if(volume < 0f) return 0f;
        else if(volume > 1f) return 1f;
        return volume;
    }

    // Use : SoundPlayer.Play(myClip) or SoundPlayer.Play(myClip, theVolume)
    public static void Play(AudioClip audio, float specificVolume = 1f) {
        
        int index = soundPlayed%6;
        soundSource[index].volume = globalVolume * RangeVolume(specificVolume);
        soundSource[index].clip = audio;
        soundSource[index].Play();
        soundPlayed++;
    }

    // Use : SoundPlayer.volume = 1.0f;
    public static float volume
    {
        get { return globalVolume; }
        set { 
            globalVolume = RangeVolume(value);
        }
    }
}