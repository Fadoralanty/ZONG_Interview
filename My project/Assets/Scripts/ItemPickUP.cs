using System;
using System.Collections;
using System.Collections.Generic;
//using FMODUnity;
using UnityEngine;

public class ItemPickUP : MonoBehaviour, IInteractable
{
    // [SerializeField] private EventReference pickUpSound;
    [SerializeField] private GameObject WorldSpaceCanvas;
    private UI _ui;
    private PlayerController _playerController;
    private Collider _collider;
    private void Start()
    {
        _playerController = GameManager.Instance.PlayerController;
        _ui = GameManager.Instance.UI;
        _collider = GetComponent<Collider>();
    }
    

    public void Interact()
    {
        OnInteractHandler();
    }
    private void OnInteractHandler()
    {
        //AudioManager.Instance.PlayOneShot(pickUpSound, transform.position);
        transform.position = _playerController.itemPivot.position;
        transform.parent = _playerController.itemPivot;
        _collider.enabled = false;
        Destroy(WorldSpaceCanvas);
        _ui.ToggleMenu();
    }

    public void ShowInteractPrompt()
    {
        _ui.ShowInteractPrompt("Pick Up");
    }

    public void HideInteractPrompt()
    {
        _ui.HideInteractPrompt();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void OnDestroy()
    {
    }
    
    public bool IsIndependent()
    {
        return false;
    }
    
}
