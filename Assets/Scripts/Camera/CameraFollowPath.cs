using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPath : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; //Start transform of the player
    [SerializeField] private GameObject parentObj; //Empty object reference. All children of this object will be used for the path following
    private List<Vector3> _locations; //Positions that the camera y will use
    private int _previousLocation = 0; //Index of start location first point
    private int _nextLocation = 1; //Index of start location second point

    //Debug visualization of the 2 points the camera is bound by in the editor
    [SerializeField] private GameObject debugObjPrevPos;
    [SerializeField] private GameObject debugObjNextPos;
    private void Awake()
    {
        _locations = new List<Vector3>(); //Initialize position array
        Transform[] transforms = parentObj.GetComponentsInChildren<Transform>(); //Get all child empty objects that have a transform from the parent object
        foreach (var childTransform in transforms)
        {
            _locations.Add(childTransform.position); //Add child transform.position to the position array
        }

        _locations.Remove(_locations[0]);
        
        //Initial movement
        var currentTransform = transform.position; //Get current transform
        var targetPos = _locations[_nextLocation]; //Get target position
        targetPos.z = currentTransform.z; //Never affect z
        targetPos.y = Vector3.MoveTowards(currentTransform, targetPos, 100 * Time.deltaTime).y;
        targetPos.x = playerTransform.position.x; //Use player x
        transform.position = targetPos;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //Do nothing if both left and right are pressed at the same time
        {
            return;
        }

        CheckValidLocationsCounters(); //Double check whether the indexes calculated last frame are still valid
        debugObjPrevPos.transform.position = _locations[_previousLocation]; //Show debug objects in the editor
        debugObjNextPos.transform.position = _locations[_nextLocation];
        Vector3 targetPos = transform.position; //Get current position

        if (Input.GetKey(KeyCode.D)) //Move right
        {
            targetPos = GetCameraPosition(_locations[_nextLocation]); //Update target position based on next location

            float distanceToNextPoint = Vector3.Distance(transform.position, _locations[_nextLocation]); //Check the distance between the camera and the next point
            if (distanceToNextPoint < 0.05) //If distance is small, go to the next set of points
            {
                _nextLocation += 1;
                _previousLocation += 1;
            }
        }

        if (Input.GetKey(KeyCode.A)) //Move left
        {
            targetPos = GetCameraPosition(_locations[_previousLocation]); //Update target position based on previous location
            float distanceToPreviousPoint = Vector3.Distance(transform.position, _locations[_previousLocation]); //Check the distance between the camera and the previous point
            if (distanceToPreviousPoint < 0.05) //If distance is small, go to the previous set of points
            {
                _nextLocation -= 1;
                _previousLocation -= 1;
            }
        }
        
        CheckValidLocationsCounters(); //Re-check whether the index values are still valid
        transform.position = targetPos; //Apply the position calculated
    }

    private Vector3 GetCameraPosition(Vector3 target)
    {
        Vector3 startingPos = transform.position;
        var targetPos = target;
        targetPos.z = startingPos.z; //Never change z
        targetPos.y = Mathf.Lerp(_locations[_previousLocation].y, _locations[_nextLocation].y, CalculateCameraSpeed()); //Lerp between the y values of the previous point and the next point, depending on how far the camera is between them in a 0-1 ratio
        targetPos.x = playerTransform.position.x; //Use player x
        return targetPos;
    }

    private float CalculateCameraSpeed()
    {
        float minX = Mathf.Min(_locations[_previousLocation].x, _locations[_nextLocation].x); //Get leftmost point position.x
        float maxX = Mathf.Max(_locations[_previousLocation].x, _locations[_nextLocation].x); //Get rightmost point position.x
        return (transform.position.x-minX)/(maxX-minX); //Current x and the x value of the point furthest to the right both get subtracted by the leftmost point. After that current x gets divided by the right point's x to create a 0-1 ratio
    }

    private void CheckValidLocationsCounters()
    {
        if (_nextLocation >= _locations.Count) //If current location is out of right bounds
        {
            _previousLocation = _locations.Count - 2;
            _nextLocation = _locations.Count - 1;
        }

        if (_previousLocation < 0) //If current location is out of left bounds
        {
            _previousLocation = 0;
            _nextLocation = 1;
        }
    }
}