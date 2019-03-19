using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ProjectileMath;

public class Archer : MonoBehaviour
{
    private GameObject projMath;


    //Vector3 vector = new Vector3(1,1,1);
    public float speed = 3; //Speed of projectile (I think)
    public float angleRadians = 0.785f; //About 45 degrees
    private Vector2 launchVelocity /*= new Vector2(1, 1)*/;
    float elevationRadians;
    float speedSq;
    float gravity = Physics.gravity.y;
    float time /*= 3*/;
    float launchVelocityVertical;
    float launchSpeedHorizontal;
    float distanceHorizontal;
    Vector2 displacement;
    float minElevationRads;
    float maxElevationRads;
    float searchStep;
    float outRadians;
    public float peakHeight = 6;
    float peakTime;
    float targetTime;
    float horizontalSpeed;
    float linearSpeed;
    Vector2 targetDisplacement;
    bool canAttack = true;

    // Use this for initialization
    void Start()
    {
        projMath = GetComponent<GameObject>();
        //ProjectileMath.Get2DVec(new Vector3(0, 0, 0));
        //Vector2 launchVelocity = new Vector2(1, 1);
        //launchVelocity = ProjectileMath.GetLaunchVelocity(speed, angleRadians);
        ProjectileMath.GetLaunchVelocity(speed, angleRadians);
        ProjectileMath.BreakLaunchVectorToAngleAndSpeed(launchVelocity, out elevationRadians, out speedSq);
        ProjectileMath.GetDisplacementAtTime(launchVelocity, gravity, time);
        ProjectileMath.GetPeakTime(launchVelocityVertical, gravity);
        ProjectileMath.GetTimeToHorizontal(launchSpeedHorizontal, distanceHorizontal);
        ProjectileMath.ComputeProjectileSpeedSq(displacement, elevationRadians, gravity, out speedSq);
        ProjectileMath.ComputeProjectileMinEffort(displacement, minElevationRads, maxElevationRads, searchStep, gravity, out outRadians, out speedSq);
        ProjectileMath.ComputeProjectileWithPeakHeight(displacement, peakHeight, gravity, out launchVelocity, out peakTime, out targetTime);
        //ProjectileMath.ComputeProjectileWithFixedHorizonalSpeed(displacement, horizontalSpeed, gravity, out launchVelocity);
        //ProjectileMath.ComputeProjectileWithFixedLinearSpeed(displacement, linearSpeed, gravity, out launchVelocity);
        //ProjectileMath.IsUnderParabola(targetDisplacement, launchVelocity, gravity);
        Debug.Log("launchVelocity:" + launchVelocity + "  __targetDisplacement:" + targetDisplacement);
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileMath.GetDisplacementAtTime(launchVelocity, gravity, time);
        /*
         GetDisplacementAtTime() will allow you to move the object kinematically via Update(),
         or just give a RigidBody the launch velocity and let it fly.
         If your linear drag is set to 0 then these will hold true in PhysX or Box2D
         */
    }

    void OnTriggerEnter2D(Collider2D col)//non-stop updating
    {
        if (col.tag == "Player")
        {
            Debug.Log("Enter" + col.gameObject.name + " : " + gameObject.name);
            canAttack = true;
            Debug.Log(canAttack);
        }
    }

    void OnTriggerExit2D(Collider2D col)//non-stop updating
    {
        if (col.tag == "Player")
        {
            Debug.Log("Exit" + col.gameObject.name + " : " + gameObject.name);
            canAttack = false;
            Debug.Log(canAttack);
        }
    }
}
