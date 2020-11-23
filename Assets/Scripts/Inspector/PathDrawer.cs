using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConnectedObjects))]
public class PathDrawer : Editor
{
    void OnSceneGUI()
    {
        ConnectedObjects connectedObjects = target as ConnectedObjects;
        if (connectedObjects == null) return;
        Transform[] transforms = connectedObjects.GetComponentsInChildren<Transform>();
        if (transforms.Length <= 0) return;


        for (int i = 2; i < transforms.Length; i++)
        {
            Vector3 firstLocation = transforms[i - 1].position;
            Vector3 lastLocation = transforms[i].position;

            Handles.color = Color.red;
            Handles.DrawLine(firstLocation, lastLocation);
        }
    }
}