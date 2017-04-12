using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class MathUtility {
    public static double NextGaussian(this System.Random random, double mu = 0, double sigma = 1) {
        var u1 = random.NextDouble();
        var u2 = random.NextDouble();

        var standardNormal = Math.Sqrt(-2f * Math.Log(u1)) * Math.Sin(2f * Math.PI * u2);
        return mu + sigma * standardNormal;
    }

    public static Vector3 Direction(Vector3 from, Vector3 to) {
        var dir = from - to;
        Debug.DrawRay(from, dir, Color.red, 10f);
        return dir;
    }

    public static bool IsWithinSight(Vector3 from, Vector3 to, float angle) {
        from.y = to.y = 0f; // Clamp the y values
        return Vector3.Angle(from, to) < angle / 2;
    }
}
