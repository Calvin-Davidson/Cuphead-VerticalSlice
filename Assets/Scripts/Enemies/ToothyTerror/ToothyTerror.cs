using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToothyTerror : MonoBehaviour
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

    Vector3 target = _toBegin ? beginPoint : endPoint;
    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    if (transform.position.Equals(target))
    {
      _toBegin = !_toBegin;
      if (_toBegin) endPointReach.Invoke();
      if (!_toBegin) beginPointReach.Invoke();
    }
  }
}
