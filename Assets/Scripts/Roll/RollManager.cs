using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    private IRollManager rollManager;

    public int activeNumber => rollManager.activeNumber;

    public Action OnRollFinished
    {
        get
        {
            return rollManager.OnRollFinished;
        }
        set
        {
            rollManager.OnRollFinished = value;
        }
    }

    void Awake()
    {
        rollManager = GetComponent<IRollManager>();
        rollManager.Initialize();
    }

    public void Roll()
    {
        rollManager.Roll();
    }

    public void Enable()
    {
        rollManager.Enable();
    }

    public void Disable()
    {
        rollManager.Disable();
    }
}
