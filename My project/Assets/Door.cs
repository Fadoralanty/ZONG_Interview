using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (other.GetComponent<PlayerController>().hasSphere)
        {
            SceneManager.LoadScene("Second Sequence");
        }
    }
}
