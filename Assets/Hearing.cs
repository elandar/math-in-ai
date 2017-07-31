using UnityEngine;
using UnityEngine.AI;

public class Hearing : MonoBehaviour {

    public bool IsHearing { get { return isHearing; } }

    public float maxHearingDistance = 10f;
    public AudioSource selectedAudio;
    private NavMeshAgent agent;
    private bool isHearing;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();   // Cache the NavMeshAgent component
    }

    private void Update() {

        // So if the audio source is playing some audio
        if (selectedAudio.isPlaying) {
            float distance = GetAudioMagnitude(selectedAudio.transform.position);
            if (distance <= maxHearingDistance) {
                isHearing = true;
            }
        } else {
            isHearing = false;
        }
    }

    private float GetAudioMagnitude(Vector3 targetPosition) {
        // Create a new NavMesh path
        NavMeshPath path = new NavMeshPath();

        // As long as the NavMeshAgent is enabled, calculate the path
        if (agent.enabled) {
            agent.CalculatePath(targetPosition, path);
        }
    
        // Create an array of points in which our NavMeshAgent sampled
        Vector3[] audioPath = new Vector3[path.corners.Length + 2];
    
        // Set the first and last points to be the source and destination
        audioPath[0] = transform.position;
        audioPath[audioPath.Length - 1] = targetPosition;
    
        // Put in all of the points of the sampled path into our array
        for (int i = 0; i < path.corners.Length; i++) {
            audioPath[i + 1] = path.corners[i];
        }

        // Our magnitude measures our total distance
        float audioMagnitude = 0f;

        // Calculate the distance between each individual point and add them up
        for(int i = 0; i < audioPath.Length - 1; i++) {
            audioMagnitude += Vector3.Distance(audioPath[i], audioPath[i + 1]);
        }

        return audioMagnitude;
    }
}
