using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    private static readonly object Padlock = new();
    
    public UI UI;
    public PlayerController PlayerController;
    public static GameManager Instance
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
