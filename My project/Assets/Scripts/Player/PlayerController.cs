using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : Entity
{
    public Transform itemPivot;
    [SerializeField] private float _moveSpeeed;
    [SerializeField] private float _sprintSpeeed;
    [SerializeField] private Transform _orientation;
    protected  override void Awake()
    {
        base.Awake();
        speed = _moveSpeeed;
    }
    private void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 movedir = _orientation.right * hor + _orientation.forward * ver;
        Move(movedir.normalized);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = _sprintSpeeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = _moveSpeeed;
        }
    }
    
}
