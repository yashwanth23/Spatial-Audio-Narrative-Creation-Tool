using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is to move the stationary audio towards the camera and move back to its intitial position
public class AudioMovementTimed : MonoBehaviour
{
    public GameObject Player;
    public float speed = 3f;

    private Vector3 startPos;

    public int index;
    public float duration;
    public bool near;

    public bool isPlayed = true;

    private float t;

    private bool delayCheck;
    

    void Start()
    {
        near = false;
        //Audio_count = 0;
        startPos = this.transform.position;
        delayCheck = false;

    }


    void Update()
    {

        //isPlayed value will be changed in StandardTimedAudio script
        if (!isPlayed)
        {
            //When the audio source is triggered it starts moving towards the camera 

            //Calculating the distance from the audio source to the camera
            float dist = Vector3.Distance(Player.transform.position, transform.position);

            //Moves close to the camera until the distance is 1
            if (dist > 1.0f && !near)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            }
            else if (dist >= 1.0f && near)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                if (transform.position == startPos)
                {
                    near = false;
                    isPlayed = true;
                }
            }

            //If the source reaches the destination position, it waits for the duration that was mentioned
            if (dist <= 1.0f)
            {
                delayCheck = true;
                StartCoroutine(Playdelay(duration, delayCheck));
            }
            
        }
        
    }

    // Duration for which the audio is set to play 
    IEnumerator Playdelay(float delay, bool check)
    {
        while (check)
        {
            yield return new WaitForSeconds(delay);
            
            near = true;
            //After the wait duration is complete the audio source moves back to its initial position
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            yield break;
            
        }
        while (!check)
        {
            near = false;
            yield break;
        }
    }
}
