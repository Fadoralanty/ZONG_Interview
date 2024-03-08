using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class PlayerController : Entity
{
    public Transform itemPivot;
    public GameObject sphere;
    public bool hasSphere;
    [SerializeField] private float _moveSpeeed;
    [SerializeField] private float _sprintSpeeed;
    [SerializeField] private Transform _orientation;
    private EventInstance playerFootSteps;
    
    protected override void Awake()
    {
        base.Awake();
        speed = _moveSpeeed;
    }

    protected void Start()
    {
        playerFootSteps = AudioManager.Instance.CreateEventInstance(FMOD_events.Instance.playerFootsteps);
    }

    private void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if (hor==0 && ver==0)
        {
            StopFootSteps();
            return;
        }
        
        Vector3 movedir = _orientation.right * hor + _orientation.forward * ver;
        Move(movedir.normalized);
        PlayFootsteps();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = _sprintSpeeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = _moveSpeeed;
        }
    }
    private void PlayFootsteps()
    {
        PLAYBACK_STATE playbackState;
        playerFootSteps.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            playerFootSteps.start();
        }
    }
    private void StopFootSteps()
    {
        playerFootSteps.stop(STOP_MODE.ALLOWFADEOUT);
    }
    
}
