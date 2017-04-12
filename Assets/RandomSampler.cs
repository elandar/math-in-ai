using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSampler : MonoBehaviour {

    public double lhs = 0f;
    public double rhs = 1f;
    public int iterationCount = 10;

    private System.Random rand;
    private List<double> randomNumbers;

    private void Start() {

        rand = new System.Random();
        randomNumbers = new List<double>();

        for (int i = 0; i < iterationCount; i++) {
            randomNumbers.Add(rand.NextGaussian(lhs, rhs));
        }

        randomNumbers.Sort();

        foreach (var number in randomNumbers) {
            Debug.Log(number);
        }
    }

}
