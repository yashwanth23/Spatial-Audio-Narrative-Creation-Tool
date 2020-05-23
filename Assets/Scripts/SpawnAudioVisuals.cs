using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAudioVisuals : MonoBehaviour
{
    public ParticleSystem AudioVisual;
    private ParticleSystem AudioEffect;

    public GameObject speaker;
    
    //If the stationary audio source is triggered, it moves towards the camera. When it moves towards the camera, audio visuals are instantiated
    //To check if the audio source is moving away from its position
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "AudioSources")
        {
            //Audio visual is instantiated when audio source moves away from its initial position
            AudioEffect = Instantiate(AudioVisual, speaker.transform.position + 0.8f*speaker.transform.forward , Quaternion.LookRotation(Vector3.up));
        }
    }

    //To check if the audio source is moving towards its initial position
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AudioSources")
        {
            //Destroy the audio visual when the audio source reaches its initial position
            Destroy(AudioEffect);
        }
    }
}
