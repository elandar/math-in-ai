using UnityEngine;
using UnityEngine.AI;

public class Hearing : MonoBehaviour {

    public float maxHearingDistance = 10f;
    public AudioSource selectedAudio;
    private NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();   // Cache the NavMeshAgent component
    }

    private void Update() {
        if (selectedAudio.isPlaying) {
            agent.isStopped = false;
            float distance = GetAudioMagnitude(selectedAudio.transform.position);
            if (distance <= maxHearingDistance) {
                agent.SetDestination(selectedAudio.transform.position);
            }
        }
    }

    private float GetAudioMagnitude(Vector3 targetPosition) {
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled) {
            agent.CalculatePath(targetPosition, path);
        }

        Vector3[] audioPath = new Vector3[path.corners.Length + 2];

        audioPath[0] = transform.position;
        audioPath[audioPath.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++) {
            audioPath[i + 1] = path.corners[i];
        }

        float audioMagnitude = 0f;

        for(int i = 0; i < audioPath.Length - 1; i++) {
            audioMagnitude += Vector3.Distance(audioPath[i], audioPath[i + 1]);
        }

        return audioMagnitude;
    }
}
