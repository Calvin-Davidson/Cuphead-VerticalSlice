using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toothy_Terror : MonoBehaviour
{
  [SerializeField] private UnityEvent endPointReach = new UnityEvent();
  [SerializeField] private UnityEvent beginPointReach = new UnityEvent();
  
  [SerializeField] private float speed;
  [SerializeField] private Vector3 endPoint;
  [SerializeField] private Vector3 beginPoint;

  private bool _toBegin = false;
  private bool _move = true;
  
  private void Update()
  {
    // If animation is playing.
    if (!_move)
    {
      return;
    }
    
    if (_toBegin)
    {
      transform.position = Vector3.MoveTowards(transform.position, beginPoint, speed * Time.deltaTime);
      if (transform.position.Equals(beginPoint))
      {
        _toBegin = false;
        beginPointReach.Invoke();
      }
    }
    else
    {
      transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
      if (transform.position.Equals(endPoint))
      {
        _toBegin = true;
        endPointReach.Invoke();
      }
    }
  }
}
