using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Flee : MonoBehaviour {

    public float fleeDistance = 10f;
    public float angleDeviation = 45f;

    private EyeSight eyeSight;
    private Hearing hearing;
    private NavMeshAgent agent;
    private bool _lock;

    private void Start() {
        eyeSight = GetComponent<EyeSight>();
        hearing = GetComponent<Hearing>();
        agent = GetComponent<NavMeshAgent>();
        _lock = false;
    }

    private void Update() {
        if (eyeSight.GameObject != null || hearing.IsHearing) {
            Vector3 destination = MathUtility.GetDestination(transform.position, 
                eyeSight.GameObject.transform.position, 
                Random.Range(-angleDeviation, angleDeviation), fleeDistance);
            if (!_lock) {
                agent.SetDestination(destination);
                _lock = true;
            }
        }
    }
}
