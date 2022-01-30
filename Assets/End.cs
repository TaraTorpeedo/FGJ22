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

    public void StartTimeline_Start() 
    {
        Start_.Play(); 
    }

    public void StartTimeline_End()
    {
        End_.Play();
    }

    public void StartTimeline_Credits()
    {
        Credits_.Play();
    }
}
