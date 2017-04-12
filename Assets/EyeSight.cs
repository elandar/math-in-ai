using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSight : MonoBehaviour, ISeeable {
    public float ViewDistance { get { return viewDistance; } }
    public float ViewAngle { get { return viewAngle; } }
    public LayerMask LayerMask { get { return layerMask; } }
    public GameObject GameObject { get { return selectedGameObject; } }

    public float viewDistance = 20f;
    public float viewAngle = 160f;
    public LayerMask layerMask;
    public int bufferSize = 32;

    private Collider[] eyeSightBuffer;
    [SerializeField]
    private GameObject selectedGameObject;
    [SerializeField]
    private bool canSearch;

    private void Start() {
        eyeSightBuffer = new Collider[bufferSize];
        selectedGameObject = null;
        canSearch = true;
    }

    public void Update() {
        if (canSearch) {
            CanSeeObject();
        }
        else {
            if (selectedGameObject != null && MathUtility.IsWithinSight(transform.position, selectedGameObject.transform.position, viewAngle / 2)) {
                canSearch = true;
                selectedGameObject = null;
            }
        }
    }

    public bool CanSeeObject() {
        int querySize = Physics.OverlapSphereNonAlloc(transform.position, viewDistance, eyeSightBuffer, layerMask);
        GameObject selectedGameObject = null;
        float distance = Mathf.Infinity;
        for (int i = 0; i < querySize; i++) {
            Collider currentObject = eyeSightBuffer[i];

            if (MathUtility.IsWithinSight(transform.forward, currentObject.transform.position, viewAngle)) {
                // Okay so our target is within actual position
                // We should check whether or not we've actually seen our target

                // Get our directional vector
                Vector3 direction = currentObject.transform.position - transform.position;
                RaycastHit hitInfo;
                Debug.DrawRay(transform.position, direction, Color.red);
                if (Physics.Raycast(transform.position, direction, out hitInfo, viewDistance, layerMask)) {
                    if (direction.sqrMagnitude <= distance) {
                        if (hitInfo.collider.gameObject == currentObject.gameObject) {
                            distance = direction.sqrMagnitude;
                            selectedGameObject = currentObject.gameObject;
                        }
                    }
                }
            }
        }

        if (selectedGameObject != null) {
            this.selectedGameObject = selectedGameObject;
            canSearch = false;
            return true;
        }
        return false;
    }
}
