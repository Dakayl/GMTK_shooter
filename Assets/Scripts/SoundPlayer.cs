using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    //static instance of the Player to check if we should keep or delete new instances
    protected static SoundPlayer Instance;
    protected static float globalVolume = 1.0f;
    protected static AudioSource soundSource;
    protected void Start()
    {            
        if (SoundPlayer.Instance == null)
        {
            //dont destroy this instance
            DontDestroyOnLoad(this);
            soundSource = gameObject.AddComponent<AudioSource>();
            //SoundPlayer is intended for small fx sounds
            soundSource.loop = false;
            soundSource.volume = RangeVolume(globalVolume);
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
        soundSource.volume = globalVolume * RangeVolume(specificVolume);
        soundSource.clip = audio;
        soundSource.Play();
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