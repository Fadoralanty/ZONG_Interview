using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;
    private static readonly object Padlock = new();
    public Bus MasterBus => _masterBus;
    private Bus _masterBus;
    public Bus MusicBus => _musicBus;
    private Bus _musicBus;
    public Bus SfxBus => _sfxBus;
    private Bus _sfxBus;
    private List<EventInstance> _eventInstances = new List<EventInstance>();
    private EventInstance _musicEventInstance;
    
    public static AudioManager Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance;
            }
        }
    }

    private void Awake()
    {
        lock (Padlock)
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
    }

    private void Start()
    {
        _masterBus = RuntimeManager.GetBus("bus:/");
        //_musicBus = RuntimeManager.GetBus("bus:/Music");
        //_sfxBus = RuntimeManager.GetBus("bus:/SFX"); 
        
        InitializeMusic(FMOD_events.Instance.ThisLevelMusic); //play bg music
    }
    
    private void InitializeMusic(EventReference eventReference)
    {
        _musicEventInstance = CreateEventInstance(eventReference);
        _musicEventInstance.start();
    }

    public void StopMusic()
    {
        _musicEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPosition) 
    { //plays a sound once till its finished without looping in the position
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance CreateEventInstance(EventReference eventReference) //creates an event to play looping sounds
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        _eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void Dispose()
    {
        foreach (var eventInstance in _eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
}
