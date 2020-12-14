using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BothersomeBlueberry : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    private bool _isStopping = false;
    private bool _toBegin = false;
  
    private void Update()
    {
        if (_isStopping) return;

        Vector3 moveTo = Vector3.zero;
        
        if (_toBegin)
        {
            moveTo.x = (-speed * Time.deltaTime);
        }
        else
        {
            moveTo.x = (speed * Time.deltaTime);
        }

        transform.position += moveTo;
    }

    public void startRotation()
    {
        _isStopping = true;
    }

    public void startWalkingBack()
    {
        _toBegin = true;
        _isStopping = false;
    }

    public void startWalkingForward()
    {
        _isStopping = false;
        _toBegin = false;
    }
}
