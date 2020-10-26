using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendBackgroundScript : MonoBehaviour
{
    public Material mat;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Debug.Log("camera position: " + lastCameraPosition);
    }
}
