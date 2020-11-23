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

        var trans = transform.position;
        var target = _locations[_nextLocation];
        target.z = trans.z;
        target.y = Vector3.MoveTowards(trans, target, 100 * Time.deltaTime).y;
        target.x = playerTransform.position.x;
        transform.position = target;
    }

    public void Update()
    {
        Vector3 startingPos = transform.position;
        Vector3 newPos = startingPos;
        
        if (Input.GetKey(KeyCode.D))
        {
            var target = _locations[_nextLocation];
            target.z = transform.position.z;
            target.y = Vector3.MoveTowards(startingPos, target, 100 * Time.deltaTime).y;
            target.x = playerTransform.position.x;
            newPos = target;
        }

        if (Input.GetKey(KeyCode.A))
        {
            var target = _locations[_previousLocation];
            target.z = transform.position.z;
            target.y = Vector3.MoveTowards(startingPos, target, 3 * Time.deltaTime).y;
            target.x = playerTransform.position.x;
            newPos = target;
        }
        transform.position = newPos;

    }
}