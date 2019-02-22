using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ProjectileMath;

public class Archer : MonoBehaviour
{
    private GameObject projMath;


    //Vector3 vector = new Vector3(1,1,1);
    public float speed = 3;
    float angleRadians = 2.45f;
    Vector2 launchVelocity = new Vector2(1, 1);
    float elevationRadians = 2.45f;
    float speedSq;
    float gravity = 9.81f;
    float time = 3;
    float launchVelocityVertical;
    float launchSpeedHorizontal;
    float distanceHorizontal;
    Vector2 displacement;
    float minElevationRads;
    float maxElevationRads;
    float searchStep;
    float outRadians;
    float peakHeight;
    float peakTime;
    float targetTime;
    float horizontalSpeed;
    float linearSpeed;
    Vector2 targetDisplacement;


    // Use this for initialization
    void Start()
    {
        projMath = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileMath.Get2DVec(new Vector3(0, 0, 0));
        launchVelocity = ProjectileMath.GetLaunchVelocity(speed, angleRadians);
        ProjectileMath.BreakLaunchVectorToAngleAndSpeed(launchVelocity, out elevationRadians, out speedSq);
        ProjectileMath.GetDisplacementAtTime(launchVelocity, gravity, time);
        ProjectileMath.GetPeakTime(launchVelocityVertical, gravity);
        ProjectileMath.GetTimeToHorizontal(launchSpeedHorizontal, distanceHorizontal);
        ProjectileMath.ComputeProjectileSpeedSq(displacement, elevationRadians, gravity, out speedSq);
        ProjectileMath.ComputeProjectileMinEffort(displacement,minElevationRads,maxElevationRads,searchStep,gravity,out outRadians,out speedSq);
        ProjectileMath.ComputeProjectileWithPeakHeight(displacement, peakHeight, gravity,out launchVelocity,out peakTime,out targetTime);
        ProjectileMath.ComputeProjectileWithFixedHorizonalSpeed(displacement, horizontalSpeed, gravity,out launchVelocity);
        ProjectileMath.ComputeProjectileWithFixedLinearSpeed(displacement,linearSpeed,gravity,out launchVelocity);
        ProjectileMath.IsUnderParabola(targetDisplacement, launchVelocity,gravity);
    }

    //void GetDisplacementAtTime()
    //{

    //}
}
