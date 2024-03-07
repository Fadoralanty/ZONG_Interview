using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    void ShowInteractPrompt();
    void HideInteractPrompt();
    Transform GetTransform();
    bool IsIndependent();
}
