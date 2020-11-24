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

    [SerializeField] private GameObject debugObjPrevPos;
    [SerializeField] private GameObject debugObjNextPos;

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
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            return;
        }

        CheckValidLocationsCounters();
        debugObjPrevPos.transform.position = _locations[_previousLocation];
        debugObjNextPos.transform.position = _locations[_nextLocation];

        Vector3 startingPos = transform.position;
        Vector3 newPos = startingPos;

        if (Input.GetKey(KeyCode.D))
        {
            newPos = GetCameraPosition(_locations[_nextLocation]);

            float dist = Vector3.Distance(transform.position, _locations[_nextLocation]);
            if (dist < 0.05)
            {
                _nextLocation += 1;
                _previousLocation += 1;
                CheckValidLocationsCounters();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            newPos = GetCameraPosition(_locations[_previousLocation]);
            float dist = Vector3.Distance(transform.position, _locations[_previousLocation]);
            if (dist < 0.05)
            {
                _nextLocation -= 1;
                _previousLocation -= 1;
                CheckValidLocationsCounters();
            }
        }

        transform.position = newPos;
    }

    private Vector3 GetCameraPosition(Vector3 targetPos)
    {
        Vector3 startingPos = transform.position;
        var target = targetPos;
        target.z = startingPos.z;
        // 4.102742985f
        //target.y = Vector3.MoveTowards(startingPos, target, CalculateCameraSpeed() * Time.deltaTime).y;
        target.y = Mathf.Lerp(_locations[_previousLocation].y, _locations[_nextLocation].y, CalculateCameraSpeed());
        target.x = playerTransform.position.x;
        return target;
    }

    private float CalculateCameraSpeed()
    {
        float diffX = Math.Abs(_locations[_previousLocation].x - _locations[_nextLocation].x);
        float diffY = Math.Abs(_locations[_previousLocation].y - _locations[_nextLocation].y);

        float minX = Mathf.Min(_locations[_previousLocation].x, _locations[_nextLocation].x);
        float maxX = Mathf.Max(_locations[_previousLocation].x, _locations[_nextLocation].x);
        return (transform.position.x-minX)/(maxX-minX);
        
//       return Mathf.Lerp(_locations[_previousLocation].y, _locations[_nextLocation].y, 4.5f);
//        return (float) Math.Sqrt((float) Math.Pow(diffX, 2) + (float) Math.Pow(diffY, 2));
    }

    private void CheckValidLocationsCounters()
    {
        if (_nextLocation >= _locations.Count)
        {
            _previousLocation = _locations.Count - 2;
            _nextLocation = _locations.Count - 1;
        }

        if (_previousLocation < 0)
        {
            _previousLocation = 0;
            _nextLocation = 1;
        }
    }
}