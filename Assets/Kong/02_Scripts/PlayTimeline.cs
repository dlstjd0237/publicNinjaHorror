using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayTimeline : MonoBehaviour
{
    PlayableDirector playable;

    private void Awake()
    {
        playable = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        playable.Play();
    }
}
