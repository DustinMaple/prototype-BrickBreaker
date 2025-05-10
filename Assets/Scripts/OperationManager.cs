using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationManager : MonoBehaviour
{

    private Dictionary<KeyCode, Operation> _bindingDic = new();
    private HashSet<Operation> _operations = new();
    
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }

        BindKeys(Operations.MoveLeft, KeyCode.A, KeyCode.LeftArrow);
        BindKeys(Operations.MoveRight, KeyCode.D, KeyCode.RightArrow);
    }

    private void BindKey(KeyCode keyCode, Operation operation)
    {
        if (_bindingDic.ContainsKey(keyCode))
        {
            _bindingDic[keyCode].UnBindKey(keyCode);
        }
        
        _bindingDic[keyCode] = operation;
        operation.BindKey(keyCode);

        _operations.Add(operation);
    }

    private void BindKeys(Operation operation, params KeyCode[] keyCodes)
    {
        foreach (KeyCode keyCode in keyCodes)
        {
            BindKey(keyCode, operation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Operation operation in _operations)
        {
            operation.Doing = CheckKeyHolding(operation);
        }
    }

    private bool CheckKeyHolding(Operation operation)
    {
        foreach (KeyCode keyCode in operation.BindKeys)
        {
            if (Input.GetKey(keyCode))
            {
                return true;
            }
        }

        return false;
    }
}

public static class Operations
{
    public static readonly Operation MoveRight = new();
    public static readonly Operation MoveLeft = new();
}

public class Operation
{
    private List<KeyCode> _bindKeys = new();
    
    public bool Doing;

    public void BindKey(KeyCode keyCode)
    {
        _bindKeys.Add(keyCode);
    }

    public void UnBindKey(KeyCode keyCode)
    {
        _bindKeys.Remove(keyCode);
    }

    public List<KeyCode> BindKeys => _bindKeys;
}