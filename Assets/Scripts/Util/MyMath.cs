using UnityEngine;

public static class MyMath
{
    public static float GetAcceleration(float position, float velocity, float posDesired, float velDesired, float accMax, float t, bool debug = false)
    {
        if(accMax == 0)
        {
            return 0;
        }
        var velocityAbs = Mathf.Abs(velocity);
        var posDiff = posDesired - position;
        var velDiff = velDesired - velocity;
        var posDiffSign = Mathf.Sign(posDiff);
        var velDiffSign = Mathf.Sign(velDiff);
        if (posDiffSign == velDiffSign && posDiff != 0 && velDiff != 0)
        {
            //Debug.Log("exit with " + posDiffSign + "," + velDiffSign + ", heh: " + posDiff + "," + velDiff);
            return posDiffSign * accMax;
        }
        var posDiffAbs = Mathf.Abs(posDiff);
        var velDiffAbs = Mathf.Abs(velDiff);
        var reachVelFastest = velDiff == 0 ? 0 : velDiffAbs / accMax;

        var reachPosFastest = posDiff == 0 ? 0 : Solve2NdDegreeEquation(0.5f * posDiffSign * accMax, velocity, -posDiff);
        if (debug)
        {

        Debug.Log("position: " + position + ", posDesired: " + posDesired + ", velocity: " + velocity + ", velDesired: " + velDesired
            + ", reach pos: " + reachPosFastest + ", reachVelFastest: " + reachVelFastest + "," + (reachVelFastest > reachPosFastest));
        }
        if (reachVelFastest > reachPosFastest)
        {
            return velDiffSign * accMax * Mathf.Min(Time.fixedDeltaTime, reachVelFastest)/Time.fixedDeltaTime;
        }
        else
        {
            return posDiffSign * accMax * Mathf.Min(Time.fixedDeltaTime, reachPosFastest) / Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Return smallest solution > 0
    /// </summary>
    public static float Solve2NdDegreeEquation(float a, float b, float c)
    {
        var sqrPiece = b * b - 4 * a * c;
        var sqrted = Mathf.Sqrt(sqrPiece);
        var negB = -b;
        var x1 = (negB + sqrted) / (2 * a);
        var x2 = (negB - sqrted) / (2 * a);
        var min = Mathf.Min(x1, x2);
        var max = Mathf.Max(x1, x2);
        return min >= 0 ? min : max;
    }
}
