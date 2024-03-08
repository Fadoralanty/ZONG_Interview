using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class box : MonoBehaviour,IInteractable
{
    private UI _ui;
    private Animator _animator;
    private bool hasInteracted;
    private PlayerController _playerController;
    [SerializeField] private BoxNum _boxNum;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform spherePivot;
    private enum BoxNum
    {
        BoxA,
        BoxB,
        BoxC
    }
    private void Start()
    {
        _ui = GameManager.Instance.UI;
        _animator = GetComponent<Animator>();
        _playerController = GameManager.Instance.PlayerController;
    }

    public void Interact()
    {
        if (hasInteracted) {return; }
        if (!_playerController.hasSphere) {return; }
        
        HideInteractPrompt();
        OnInteractHandler();
    }

    private void OnInteractHandler()
    {
        switch (_boxNum)
        {
            case BoxNum.BoxA:
                _particleSystem.Play();
                _ui.screenMessage.SetActive(true);
                _ui.screenMessageText.text = "You Have Dropped On Box A";
                break;
            case BoxNum.BoxB:
                _particleSystem.Play();
                _ui.screenMessage.SetActive(true);
                _ui.screenMessageText.text = "You Have Dropped On Box B";
                break;
            case BoxNum.BoxC:
                SceneManager.LoadScene("Third Sequence");
                break;
        }

        _playerController.sphere.transform.parent = spherePivot;
        _playerController.sphere.transform.position = spherePivot.position;
        _playerController.hasSphere = false;
        hasInteracted = true;
    }

    public void ShowInteractPrompt()
    {
        if (hasInteracted) {return; }
        _ui.ShowInteractPrompt("Drop");
    }

    public void HideInteractPrompt()
    {
        _ui.HideInteractPrompt();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsIndependent()
    {
        return false;
    }
}
