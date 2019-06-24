using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//
// Audio Manager handles all the audio. Use the functions to play Audio!
//

// TODO: Improve code some more

// Struct of an audio clip.
[System.Serializable]
public struct Sound
{
    public string clipName;
    public AudioClip clip;
    public AudioMixerGroup mixer;

    [Range(0f, 10f)]
    public float volume;
    [Range(0.5f, 5f)]
    public float pitch;

    public bool loop;
}

// Audiomanager Component
public class AudioManager : MonoBehaviour
{
    public static AudioManager main;
    public Sound[] sounds;

    void Awake()
    {
        main = this;
    }

    // This method assigns the audio on a gameobject and destroys itself after its done.
    // "is2D" plays the audio in 2D
    public void Play(int audio, float volume, float pitch, GameObject go, bool is2D)
    {
        //Create AudioSource to Gameobject, if it doesn't have one already.
        if (!go.GetComponent<AudioSource>())
        {
            go.AddComponent<AudioSource>();
        }

        AudioSource tempAudio = go.GetComponent<AudioSource>();

        // Assign attributes to the Audiosource
        try
        {
            tempAudio.clip = sounds[audio].clip;
            tempAudio.volume = volume;
            tempAudio.pitch = pitch;
            tempAudio.loop = sounds[audio].loop;
            tempAudio.outputAudioMixerGroup = sounds[audio].mixer;
            tempAudio.spatialBlend = (is2D ? 0 : 1f);
            //tempAudio.playOnAwake = true;
            tempAudio.Play();
            
        } catch
        {
            Debug.Log("Couldn't play Audio ID " + audio);
            Destroy(tempAudio);
        }

        // Auto-destroy based on clip length
        Destroy(tempAudio, tempAudio.clip.length);
    }

    // Play Audio without the need of changing its audioclip attributes
    public void Play(int audio, GameObject go, bool is2D)
    {
        Play(audio, sounds[audio].volume, sounds[audio].pitch, go, is2D);
    }

    // Play Audio based on clip name
    public void Play(string name, float volume, float pitch, GameObject go, bool is2D)
    {
        int id = getClipByName(name);
        Play(id, sounds[id].volume, sounds[id].pitch, go, is2D);

    }

    // Play Audio based on clip name, minus the volume
    public void Play(string name, float pitch, GameObject go, bool is2D)
    {
        int id = getClipByName(name);
        Play(id, sounds[id].volume, pitch, go, is2D);
    }

    // Play Audio based on clip name, minus the attributes
    public void Play(string name, GameObject go, bool is2D)
    {
        Play(getClipByName(name), go, is2D);
    }

    // Fetch Audioclip based on string comparison
    private int getClipByName(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].clipName == name)
            {
                return i;
            }
        }
        return 0;
    }
}
