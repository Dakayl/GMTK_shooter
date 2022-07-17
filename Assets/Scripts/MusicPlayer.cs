using UnityEngine;
using System.Collections;
public class MusicPlayer : MonoBehaviour
{
    //static instance of the Player to check if we should keep or delete new instances
    public static MusicPlayer Instance;
    protected static float globalVolume = 0.5f;
    protected static AudioSource musicSource;
    private const float fadeOutDuration = 1.2f;
    private const float fadeInDuration = 1.2f;  
    private static AudioClip nextMusicClip;
    protected void Awake()
    {            
        if (MusicPlayer.Instance == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            //MusicPlayer is intended for small fx sounds
            musicSource.loop = true;
            musicSource.volume = RangeVolume(globalVolume);
            //save a reference to this instance in the static variable
            MusicPlayer.Instance = this;
        }
        else
        {
            //Only one instance
            Destroy(this.gameObject);
        }
    }

    public IEnumerator FadeOut()
    {
        float currentTime = 0;
        if(musicSource.isPlaying) {
            while (currentTime < fadeOutDuration)
            {
                currentTime += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(globalVolume, 0, currentTime / fadeOutDuration);
                yield return null;
            }
            musicSource.Stop();
        }        
        StartCoroutine(this.FadeIn());
        yield break;
    }

    public IEnumerator FadeIn()
    {
        float currentTime = 0;
        musicSource.clip = nextMusicClip;
        musicSource.Play();
        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, globalVolume, currentTime / fadeInDuration);
            yield return null;
        }
        yield break;
    }

    private static float RangeVolume(float volume) {
        if(volume < 0f) return 0f;
        else if(volume > 1f) return 1f;
        return volume;
    }

    // Use : MusicPlayer.Play(myClip) or MusicPlayer.Play(myClip, theVolume)
    public void Play(AudioClip audioClip) {
        if(audioClip != null) {
            nextMusicClip = audioClip;
            StartCoroutine(FadeOut());
        }
    }

    public void Stop(){
         musicSource.Stop();
    }
    // Use : MusicPlayer.volume = 1.0f;
    public static float volume
    {
        get { return globalVolume; }
        set { 
            globalVolume = RangeVolume(value);
        }
    }
}