using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSight : MonoBehaviour, ISeeable {
    public float ViewDistance { get { return viewDistance; } }
    public float ViewAngle { get { return viewAngle; } }
    public LayerMask LayerMask { get { return layerMask; } }

    public float viewDistance = 20f;
    public float viewAngle = 160f;
    public LayerMask layerMask;
    public int bufferSize = 32;

    private Collider[] eyeSightBuffer;
    private GameObject selectedGameObject;

    private void Start() {
        eyeSightBuffer = new Collider[bufferSize];
        selectedGameObject = null;
    }

    public bool CanSeeObject() {
        int querySize = Physics.OverlapSphereNonAlloc(transform.position, viewDistance, eyeSightBuffer, layerMask);
        GameObject selectedGameObject = null;
        for (int i = 0; i < querySize; i++) {
            Collider currentObject = eyeSightBuffer[i];
            
            if (MathUtility.IsWithinSight(transform.forward, currentObject.transform.position, viewAngle)) {
                // Okay so our target is within actual position
                // We should check whether or not we've actually seen our target

                // Get our directional vector
                Vector3 direction = currentObject.transform.position - transform.position;

                RaycastHit hitInfo;
                if (Physics.Raycast(transform.position, direction, out hitInfo, viewDistance, layerMask)) {

                    // TODO: Generally you can perform any kind of comparison operator here
                    // Compare the gameObject we've compared to the gameObject the raycast hit
                    if (hitInfo.collider.gameObject == currentObject.gameObject) {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
