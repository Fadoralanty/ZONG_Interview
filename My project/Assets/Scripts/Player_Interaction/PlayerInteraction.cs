using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 4;
    [SerializeField] private LayerMask interactLayer;
    private IInteractable _closest = null;
    private IInteractable _lastClosest = null;

    private void FixedUpdate()
    {
        _closest = GetClosestInteractable();

        if (_closest == null && _lastClosest != null)
        {
            _lastClosest.HideInteractPrompt();
            _lastClosest = null;
            return;
        }
        if (_closest == null) return;
        // if closest no es null
        if (_closest.IsIndependent()) return;
        
        if (_lastClosest == _closest)
        {
            _closest.ShowInteractPrompt();
            return;
        }
        if (_lastClosest != null)
        {
            _lastClosest.HideInteractPrompt();
        }
        _closest.ShowInteractPrompt();
        _lastClosest = _closest;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _closest != null)
        {
            _closest.Interact();
        }
    }

    public IInteractable GetClosestInteractable()
    {
        List<IInteractable> interactablesList = new List<IInteractable>();
        Collider[] closestColliders = Physics.OverlapSphere(transform.position, interactRange, interactLayer);
        if (closestColliders.Length == 0) return null;
        foreach (var collider in closestColliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactablesList.Add(interactable);
            }
        }
    
        if (interactablesList.Count == 0) return null;
        IInteractable closestInteractable = null;
        foreach (var interactable in interactablesList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else if ((transform.position - interactable.GetTransform().position).sqrMagnitude <
                     (transform.position - closestInteractable.GetTransform().position).sqrMagnitude)
            // else if (Vector3.Distance(transform.position,interactable.GetTransform().position) < 
            //          Vector3.Distance(transform.position,closestInteractable.GetTransform().position))
            {
                closestInteractable = interactable;
            }

        }

        return closestInteractable;
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
    #endif
}
