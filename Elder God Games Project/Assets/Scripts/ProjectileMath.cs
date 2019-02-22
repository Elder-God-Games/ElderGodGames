using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class ProjectileMath
{
	public static Vector2 Get2DVec(Vector3 vector)
	{
		var s = vector;
		var sH = Mathf.Sqrt(s.x*s.x + s.z*s.z);
		return new Vector2(sH, s.y);
	}

	//public static Vector3 Get3DVec(Vector2 vector, Vector3 targetDisplacement)
	//{
 //       targetDisplacement.y = 0;
 //       Vector3 horizontalNormal = targetDisplacement.normalized;

	//	return new Vector3( vector.x * horizontalNormal.x, 
	//						vector.y, 
	//						vector.x * horizontalNormal.z);
	//}

	public static Vector2 GetLaunchVelocity(float speed, float angleRadians)
	{
		return new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)) * speed;
	}
	
	//public static Vector3 GetLaunchVelocity(Vector3 targetDisplacement, float angleRadians, float speed)
	//{
 //       targetDisplacement.y = 0;
 //       Vector3 horizontalNormal = targetDisplacement.normalized;
		
	//	float cosA = Mathf.Cos(angleRadians);
	//	float sinA = Mathf.Sin(angleRadians);

	//	return new Vector3(horizontalNormal.x * cosA, sinA, horizontalNormal.z * cosA) * speed;
	//}
	
	public static void BreakLaunchVectorToAngleAndSpeed(Vector2 launchVelocity, out float elevationRadians, out float speedSq)
	{
		elevationRadians = Mathf.Atan2(launchVelocity.y, launchVelocity.x);
		speedSq = Vector2.SqrMagnitude(launchVelocity);
	}

	//public static void BreakLaunchVectorToAngleAndSpeed(Vector3 launchVelocity, out float elevationRadians, out float speedSq)
	//{
	//	float horizontal = Mathf.Sqrt(launchVelocity.x * launchVelocity.x + launchVelocity.z * launchVelocity.z);
	//	BreakLaunchVectorToAngleAndSpeed(new Vector2(horizontal, launchVelocity.y), out elevationRadians, out speedSq);
	//}

	//public static Vector3 GetDisplacementAtTime(Vector3 launchVelocity, float gravity, float time)
	//{
	//	Vector3 ret = launchVelocity * time;
	//	ret.y += 0.5f * gravity * time * time;
	//	return ret;
	//}

	public static Vector2 GetDisplacementAtTime(Vector2 launchVelocity, float gravity, float time)
	{
		Vector2 ret = launchVelocity * time;
		ret.y += 0.5f * gravity * time * time;
		return ret;
	}

	public static float GetPeakTime(float launchVelocityVertical, float gravity)
	{
		return -launchVelocityVertical / gravity;
	}

	public static float GetTimeToHorizontal(float launchSpeedHorizontal, float distanceHorizontal)
	{
		return distanceHorizontal / launchSpeedHorizontal;
	}

	//public static bool ComputeProjectileSpeedSq(Vector3 displacement, float elevationRadians, float gravity, out float speedSq)
	//{
	//	return ComputeProjectileSpeedSq(Get2DVec(displacement), elevationRadians, gravity, out speedSq);
	//}

	public static bool ComputeProjectileSpeedSq(Vector2 displacement, float elevationRadians, float gravity, out float speedSq)
	{
		speedSq = 0.0f;
		var a = elevationRadians;
		var s = displacement;

		// don't throw backwards - also avoids ÷0 errors for cos(pi)=0
		if(a >= Mathf.PI || a <= -Mathf.PI)
			return false;

		float tanA = Mathf.Tan(a);
		float cosA = Mathf.Cos(a);

		// Angle is too low to hit with -ve gravity - also avoids 0 errors for Sy=Sx*tA
		if(s.x * tanA <= s.y)
			return false;

		speedSq = ((gravity * s.x * s.x) / (2.0f * cosA * cosA))  /  (s.y - s.x * tanA);

		return true;
	}

	//public static bool ComputeProjectileMinEffort(Vector3 displacement, float minElevationRads, float maxElevationRads, float searchStep, float gravity, out float outRadians, out float speedSq)
	//{
	//	return ComputeProjectileMinEffort(Get2DVec(displacement), minElevationRads, maxElevationRads, searchStep, gravity, out outRadians, out speedSq);
	//}

	public static bool ComputeProjectileMinEffort(Vector2 displacement, float minElevationRads, float maxElevationRads, float searchStep, float gravity, out float outRadians, out float speedSq)
	{
		float targetRads = Mathf.Atan2(displacement.y, displacement.x);
		minElevationRads = Mathf.Max(minElevationRads, targetRads);
		maxElevationRads = Mathf.Min(maxElevationRads, 0.5f*Mathf.PI);

		speedSq = float.MaxValue;
		outRadians = minElevationRads;

		for(float r=minElevationRads+searchStep; r < maxElevationRads; r += searchStep)
		{
			float candidateSpeedSq = 0.0f;

			if(ComputeProjectileSpeedSq(displacement, r, gravity, out candidateSpeedSq) && candidateSpeedSq < speedSq)
			{
				speedSq = candidateSpeedSq;
				outRadians = r;
			}
			else if (outRadians < float.MaxValue)
			{
				// if we fail to get better, then we've gone past optimal
				return true;
			}
		}

		return outRadians < float.MaxValue;
	}

	public static bool ComputeProjectileWithPeakHeight(Vector2 displacement, float peakHeight, float gravity, out Vector2 launchVelocity, out float peakTime, out float targetTime)
	{
		launchVelocity = Vector2.zero;

		Assert.IsTrue(peakHeight > displacement.y, "peakHeight must be higher than the target");

		launchVelocity.y = Mathf.Sqrt(-2.0f * gravity * peakHeight);

		float fallDisp = displacement.y - peakHeight;
		float invG = 1.0f / gravity;

		float tPeak = -launchVelocity.y * invG;
		float tFall = Mathf.Sqrt(2 * fallDisp * invG);
		float tTotal = tPeak + tFall;

		launchVelocity.x = displacement.x / tTotal;

		peakTime = tPeak;
		targetTime = tTotal;
		return true;
	}

	//public static bool ComputeProjectileWithFixedHorizonalSpeed(Vector3 displacement, float horizontalSpeed, float gravity, out Vector2 launchVelocity)
	//{
	//	return ComputeProjectileWithFixedHorizonalSpeed(Get2DVec(displacement), horizontalSpeed, gravity, out launchVelocity);
	//}

	public static bool ComputeProjectileWithFixedHorizonalSpeed(Vector2 displacement, float horizontalSpeed, float gravity, out Vector2 launchVelocity)
	{
		float t = displacement.x / horizontalSpeed;
		launchVelocity.x = horizontalSpeed;
		launchVelocity.y = (displacement.y / t) - (0.5f*gravity*t);

		return true;
	}

	//public static bool ComputeProjectileWithFixedLinearSpeed(Vector3 displacement, float linearSpeed, float gravity, out Vector2 launchVelocity)
	//{
	//	return ComputeProjectileWithFixedLinearSpeed(Get2DVec(displacement), linearSpeed, gravity, out launchVelocity);
	//}

	public static bool ComputeProjectileWithFixedLinearSpeed(Vector2 displacement, float linearSpeed, float gravity, out Vector2 launchVelocity)
	{
		float horizontalSpeed = linearSpeed * displacement.x / displacement.magnitude;
		return ComputeProjectileWithFixedHorizonalSpeed(displacement, horizontalSpeed, gravity, out launchVelocity);
	}

	public static bool IsUnderParabola(Vector2 targetDisplacement, Vector2 launchVelocity, float gravity)
	{
		Assert.IsTrue(launchVelocity.x > 0.0f, "Parabola needs a positive X in the launch velocity");

		float t = targetDisplacement.x / launchVelocity.x;
		float yt = (launchVelocity.y * t) + (0.5f * gravity * t*t);

		return targetDisplacement.y < yt;
	}

	//public static bool IsUnderParabola(Vector3 targetDisplacement, Vector2 launchVelocity, float gravity)
	//{
	//	return IsUnderParabola(Get2DVec(targetDisplacement), launchVelocity, gravity);
	//}
}