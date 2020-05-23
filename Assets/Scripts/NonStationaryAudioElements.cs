using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonStationaryAudioElements : MonoBehaviour
{
    public GameObject MovingSource;
    
    private static float storyDuration;
    private GameObject movingAudio;

    //Creating custom struct for Input values of Non-stationary audio sources
    //Add System.Serializable to make it appear on the editor
    [System.Serializable]
    public struct MovingAudio_Params
    {
        //Attributes included are audioclip, time when the audio should start and volume of the audio clip
        public AudioClip clip;
        public float startTime;
        public float volume;

        public MovingAudio_Params(AudioClip aclip, float astartTime, float avolume)
        {
            clip = aclip;
            startTime = astartTime;
            volume = avolume;
        }
    }

    public MovingAudio_Params[] MovingAudioClips;
    
    // Update is called once per frame
    void Update()
    {
        //Get the timer value from StandardTimedAudio script
        storyDuration = StandardTimedAudio.storyTimer;

        //Check if the mentioned time for each Non-stationary audio source passed the timer
        for (int i = 0; i < MovingAudioClips.Length; i++)
        {
            timeCheck(MovingAudioClips[i]);
        }
    }

    private void timeCheck(MovingAudio_Params Ar)
    {
        AudioClip clip = Ar.clip;
        float start_time = Ar.startTime;
        float volume = Ar.volume;

        if (storyDuration >= start_time && storyDuration < start_time + Time.deltaTime)
        {
            //Instantiate Moving source at a random location on a circle of radius 7 when timer passes the mentioned time 
            float theta = Random.Range(0, 2*Mathf.PI);
            Vector3 startPosition = new Vector3(7f * Mathf.Cos(theta), 2.5f, 7f * Mathf.Sin(theta));
            movingAudio = Instantiate(MovingSource, startPosition, Quaternion.identity);

            //Place the clip on the moving source and play 
            movingAudio.GetComponent<AudioSource>().clip = clip;
            movingAudio.GetComponent<AudioSource>().Play();
            movingAudio.GetComponent<AudioSource>().volume = volume;
            

        }
    }
    
}
