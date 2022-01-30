using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class End : MonoBehaviour
{
    public PlayableDirector Start_;
    public PlayableDirector End_;
    public PlayableDirector Credits_;

    private void Start()
    {
        Start_ = transform.Find("timelineStart").GetComponent<PlayableDirector>();
        End_ = transform.Find("timelineEnd").GetComponent<PlayableDirector>();
        Credits_ = transform.Find("timelineCredits").GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartTimelineStart();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartTimelineEnd();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartTimelineCredits();
        }
    }

    public void StartTimelineStart() 
    {
        Start_.Play(); 
    }

    public void StartTimelineEnd()
    {
        End_.Play();
    }

    public void StartTimelineCredits()
    {
        Credits_.Play();
    }
}
