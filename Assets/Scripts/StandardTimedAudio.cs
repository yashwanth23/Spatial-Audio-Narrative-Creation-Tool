using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************
 * Indexes are the 3D locations in space around the camera 
 * The convention is as follows
 * Index: 0 - North
 *        1 - North East
 *        2 - East
 *        3 - South East
 *        4 - South
 *        5 - South West
 *        6 - West
 *        7 - North West
 *        8 - Up
 **************************************/

public class StandardTimedAudio : MonoBehaviour
{
    public GameObject BackgroundAudio;
    public GameObject[] SpatialAudioElements;

    //Creating custom struct for Input values of interactive stationary audio sources
    //Add System.Serializable to make it appear on the editor
    [System.Serializable]
    public struct SpatialAR_params
    {
        //Attributes included are audioclip, time when the audio should start, duration of the clip, volume and location of the audio clip in 3D space which are defined by indexes
        public AudioClip clip;
        public float startTime;
        public float duration;
        public float volume;
        public int index;

        public SpatialAR_params(AudioClip aclip, float astartTime, float aduration, float avolume, int aindex)
        {
            clip = aclip;
            startTime = astartTime;
            duration = aduration;
            volume = avolume;
            index = aindex;
        }
    }

    public SpatialAR_params[] SpatialAudioClips;

    private bool storyStart = false;
    public static float storyTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //When background audio starts playing story starts
        if (BackgroundAudio.GetComponent<AudioSource>().isPlaying)
        {
            storyStart = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (storyStart)
        {
            //Starting story timer
            storyTimer += Time.deltaTime;
            Debug.Log(storyTimer);
        }

        //Check if the mentioned time for each Stationary audio source passed the timer
        for (int i = 0; i < SpatialAudioClips.Length; i++)
        {
            timeCheck(SpatialAudioClips[i]);
        }
        
    }

    private void timeCheck(SpatialAR_params Ar)
    {
        AudioClip clip = Ar.clip;
        float start_time = Ar.startTime;
        float duration = Ar.duration;
        float volume = Ar.volume;
        int index = Ar.index;

        if (storyTimer >= start_time && storyTimer <= start_time + Time.deltaTime)
        {
            //When timer passes the mentioned time of the audio source, it triggers the audio source movement based on the variables below
            SpatialAudioElements[index].GetComponent<AudioMovementTimed>().isPlayed = false;
            SpatialAudioElements[index].GetComponent<AudioMovementTimed>().near = false;
            SpatialAudioElements[index].GetComponent<AudioMovementTimed>().duration = duration;

            //Play audio clip that is attached on the script in the inspector
            SpatialAudioElements[index].GetComponent<AudioSource>().clip = clip;
            SpatialAudioElements[index].GetComponent<AudioSource>().Play();
            SpatialAudioElements[index].GetComponent<AudioSource>().volume = volume;

        }
    }
   
    
}
