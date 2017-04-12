using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Flee : MonoBehaviour {

    private EyeSight eyeSight;
    public float fleeDistance = 10f;
    public float angleDeviation = 45f;

    private NavMeshAgent agent;
    private bool _lock;

    private void Start() {
        eyeSight = GetComponent<EyeSight>();
        agent = GetComponent<NavMeshAgent>();
        _lock = false;
    }

    private void Update() {
        if (eyeSight.GameObject != null) {
            var destination = MathUtility.GetDestination(transform.position, eyeSight.GameObject.transform.position, Random.Range(-angleDeviation, angleDeviation), fleeDistance);
            if (!_lock) {
                agent.SetDestination(destination);
                _lock = true;
            }
        }
    }
}
