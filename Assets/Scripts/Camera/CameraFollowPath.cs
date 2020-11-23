using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPath : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject parentObj;
    [SerializeField] private float maxDistanceFromPlayer = 0;
    private List<Vector3> _locations;
    private int _previousLocation = 0;
    private int _nextLocation = 1;

    private void Awake()
    {
        _locations = new List<Vector3>();
        Transform[] transforms = parentObj.GetComponentsInChildren<Transform>();
        foreach (var transform1 in transforms)
        {
            _locations.Add(transform1.position);
        }
        _locations.Remove(_locations[0]);
    }

    public void Update()
    {
        Transform trans = transform;
        Vector3 newPos = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
        {
            var target = _locations[_nextLocation];
            newPos.z += transform.position.z;
            newPos.y += Vector3.MoveTowards(trans.position, target, 3 * Time.deltaTime).y;
            newPos.x += playerTransform.position.x;

            if (Vector3.Distance(transform.position, _locations[_nextLocation]) < 0.001)
            {
                _previousLocation += 1;
                _nextLocation += 1;
                if (_nextLocation > _locations.Count) _nextLocation = _locations.Count;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            var target = _locations[_previousLocation];
            newPos.z = transform.position.z;
            newPos.y = Vector3.MoveTowards(trans.position, target, 3 * Time.deltaTime).y;
            newPos.x = playerTransform.position.x;
            trans.position = target;
            if (Vector3.Distance(trans.position, _locations[_previousLocation]) < 0.001)
            {
                _previousLocation -= 1;
                _nextLocation -= 1;
                if (_previousLocation < 0) _previousLocation = 0;
            }
        }
        
    }
}