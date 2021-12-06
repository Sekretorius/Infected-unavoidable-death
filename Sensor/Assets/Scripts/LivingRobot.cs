using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRobot : Robot
{
    [SerializeField] private RobotData healthyRobotData;
    [SerializeField] private RobotData infectedRobotData;

    [SerializeField] private bool isInfected = false;
    
    private readonly float InfectDistance = 1.5f;
    private readonly float AvoidDistance = 1.5f;
    private readonly float MaxFallowTurnDifference = 10f;
    private readonly float InfectedTurnSpeedModifier = 1.2f;
    private readonly float InfectedLivespan = 10f;

    private bool isTurning = false;
    private int turnDirection = 1;
    private float infectedTime = 0;

    private LineSendorSide lastDetectedSide = LineSendorSide.None;
    private System.Action robotBehavior;

    private NearestObject distanceSensorResult;
    private LineSensorData lineSensorResult;

    private void Awake()
    {
        if (isInfected) Infect();
        else
        {
            //healthyRobotData.SetValues(this);
            robotBehavior = HealthyRobotBehavior;
        }

        communicationDevice.OnMessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(Robot sender, Message message)
    {
        switch (message)
        {
            case Message.Infect:
                Infect();
                break;
        }
    }

    private void FixedUpdate()
    {
        distanceSensorResult = NearestObject;
        lineSensorResult = IsLineDetected;

        robotBehavior.Invoke();
    }

    public void Infect()
    {
        isInfected = true;
        robotBehavior = InfectedRobotBehavior;

        infectedRobotData.SetValues(this);
    }
    public void HealthyRobotBehavior()
    {
        if (HandleLineDetection()) return;

        RandomMovement();
    }

    public void InfectedRobotBehavior()
    {
        if(infectedTime > InfectedLivespan)
        {
            Die();
            return;
        }

        infectedTime += Time.fixedDeltaTime;

        if (HandleLineDetection()) return;
        
        if (distanceSensorResult.DetectedObject != null && distanceSensorResult.DetectedObject.TryGetComponent(out LivingRobot livingRobot))
        {
            if(!livingRobot.isInfected)
            {
                if (distanceSensorResult.Distance <= InfectDistance)
                {
                    communicationDevice.SendMessage(this, livingRobot, Message.Infect);
                }
                Fallow(livingRobot.transform.position);
                return;
            }
        }
        RandomMovement();
    }

    private bool HandleLineDetection()
    {
        
        if (lineSensorResult.IsDetected)
        {
            if (lastDetectedSide == LineSendorSide.None)
            {
                lastDetectedSide = lineSensorResult.LineSendorSide;
                turnDirection = lineSensorResult.LineSendorSide == LineSendorSide.Left ? 1 : -1;
            }
            Turn(turnDirection);
            return true;
        }
        lastDetectedSide = LineSendorSide.None;
        return false;
    }

    private void RandomMovement()
    {
        if (distanceSensorResult.Distance > AvoidDistance)
        {
            bool changeActionState = Random.Range(0, isTurning ? 30 : 10) == 0;
            Move(transform.forward);

            if (changeActionState)
            {
                turnDirection = Random.Range(0, 2) == 0 ? -1 : 1;
                isTurning = !isTurning;
            }

            if (isTurning)
            {
                Turn(turnDirection);
            }
        }
        else
        {
            isTurning = true;
            Turn(turnDirection);
        }
    }

    private void Fallow(Vector3 position)
    {
        float angleDifference = Vector3.SignedAngle(transform.forward, position - transform.position, transform.up);

        if (Mathf.Abs(angleDifference) > MaxFallowTurnDifference)
        {
            Turn(System.Math.Sign(angleDifference), InfectedTurnSpeedModifier);
        }
        else
        {
            if(Mathf.Abs(angleDifference) <= MaxFallowTurnDifference)
            {
                Turn(System.Math.Sign(angleDifference));
            }
            Move(transform.forward);
        }
    }

    private void Die()
    {

    }
}
