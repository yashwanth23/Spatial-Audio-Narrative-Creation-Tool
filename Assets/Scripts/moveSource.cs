using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSource : MonoBehaviour
{
    public float speed;
    public float rotate_speed;
    public bool isStarted;
    private bool isRotated;
    private GameObject SceneCamera;
    private Vector3 startPos;


    private void Awake()
    {
        //As soon as the game object is instantiated, isStarted is set to true
        isStarted = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        isRotated = false;
        //Find the camera object
        SceneCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //Store the starting position of the moving source
        startPos = this.transform.position;
    }

    void Update()
    {
        //Run these lines when the move source is instantiated
        if (isStarted)
        {
            float dist = Vector3.Distance(SceneCamera.transform.position, transform.position);
            //In order to hear the audio the audio source needs to move towards the camera
            //For a moving source, the audio source moves towards the camera to a certain distance and then starts rotating around the camera until it finishes one complete rotation
            
            if(dist > 2f && !isRotated)
            {
                transform.position = Vector3.MoveTowards(transform.position, SceneCamera.transform.position, speed * Time.deltaTime);
            }
            else if(dist <= 2f && !isRotated)
            {
                transform.RotateAround(SceneCamera.transform.position, Vector3.up, rotate_speed * Time.deltaTime);
                if (transform.rotation.y <= 0 && transform.rotation.y >= -1.5)
                {
                    isRotated = true;
                }
            }
            else if (isRotated)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            }
            
        }
    }
}
