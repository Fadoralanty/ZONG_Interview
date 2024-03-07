using System;
using System.Collections;
using System.Collections.Generic;
//using FMODUnity;
using UnityEngine;

public class UI : MonoBehaviour
{
    //Different UI Screens
    [SerializeField] private GameObject MenuScreenUI;
    [SerializeField] private GameObject PauseMenuScreenUI;
    [SerializeField] private InteractPrompt InteractPrompt;

    // [SerializeField] private EventReference OpenInventorySFX;
    // [SerializeField] private EventReference CloseInventorySFX;
    // [SerializeField] private EventReference OpenPauseSFX;
    // [SerializeField] private EventReference ClosePauseSFX;
    
    public bool IsMenuOpen => _isMenuOpen;
    private bool _isMenuOpen;
    private bool _isGamePaused;
    private bool _isPauseMenuOpen;
    

    private void Start()
    {
        _isGamePaused = false;

        MenuScreenUI.SetActive(_isMenuOpen);
        
        PauseMenuScreenUI.SetActive(_isPauseMenuOpen);

        HideInteractPrompt();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        
    }

    public void ShowInteractPrompt(string text)
    {
        InteractPrompt.gameObject.SetActive(true);
        InteractPrompt.promptText.text = text;
    }    
    public void HideInteractPrompt()
    {
        InteractPrompt.gameObject.SetActive(false);
    }
    
    public void ToggleMenu()
    {
        if (_isGamePaused && !_isMenuOpen) return;
        
        _isMenuOpen = !_isMenuOpen;

        if (_isMenuOpen)
        {
            //AudioManager.Instance.PlayOneShot(OpenInventorySFX,transform.position);
        }
        else
        {
            //AudioManager.Instance.PlayOneShot(CloseInventorySFX,transform.position);
        }
        MenuScreenUI.SetActive(_isMenuOpen);
    }
    public void TogglePauseMenu()
    {
        if (_isGamePaused && !_isPauseMenuOpen) return;

        _isPauseMenuOpen = !_isPauseMenuOpen;
        if (_isPauseMenuOpen)
        {
            //AudioManager.Instance.PlayOneShot(OpenPauseSFX,transform.position);
        }
        else
        {
           // AudioManager.Instance.PlayOneShot(ClosePauseSFX,transform.position);
        }
        PauseGame(_isPauseMenuOpen);
        PauseMenuScreenUI.SetActive(_isPauseMenuOpen);
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0f : 1f;
        _isGamePaused = value;
        Cursor.lockState = value? CursorLockMode.None : CursorLockMode.Locked;

    }
    
    private void OnDisable()
    {
        PauseGame(false);
    }
    private void OnDestroy()
    {
        MenuScreenUI = null;
        PauseMenuScreenUI = null;
        InteractPrompt = null;
    }
}
