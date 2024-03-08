using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMOD_events : MonoBehaviour
{
    [field: Header("Player SFX")] 
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: Header("Music")] 
    [field: SerializeField] public EventReference ThisLevelMusic { get; private set; }
    private static FMOD_events _instance = null;
    private static readonly object Padlock = new();
    
    public static FMOD_events Instance
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

}
