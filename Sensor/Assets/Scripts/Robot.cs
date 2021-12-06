using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Robot : MonoBehaviour
{
    public List<DistanceSensor> DistanceSensors => distanceSensors;
    public List<LineSensor> LineSensors => lineSensors;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float TurnSpeed { get => turnSpeed; set => turnSpeed = value; }

    [Header("Sensors"), Space()]
    [SerializeField] protected List<DistanceSensor> distanceSensors = new List<DistanceSensor>();
    [SerializeField] protected List<LineSensor> lineSensors = new List<LineSensor>();
    [SerializeField] protected CommunicationDevice communicationDevice;

    [Header("Robot parameters"), Space()]
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float turnSpeed = 5f;

    public CommunicationDevice CommunicationDevice => communicationDevice;
    public struct LineSensorData
    {
        public bool IsDetected { get; set; }
        public LineSendorSide LineSendorSide { get; set; }

        public LineSensorData(LineSendorSide data, bool isDetected)
        {
            LineSendorSide = data;
            IsDetected = isDetected;
        }
    }

    protected LineSensorData IsLineDetected
    {
        get
        {
            foreach (LineSensor sensor in lineSensors)
            {
                if (sensor.IsBlack) return new LineSensorData(sensor.LineSendorSide, true);
            }
            return new LineSensorData(LineSendorSide.None, false);
        }
    }

    protected NearestObject NearestObject
    {
        get
        {
            NearestObject nearestObject = new NearestObject(float.MaxValue, null);
            foreach (DistanceSensor sensor in distanceSensors)
            {
                if (sensor.DetectedObject != null) 
                {
                    nearestObject = nearestObject.Distance > sensor.DetectedObject.Distance ? sensor.DetectedObject : nearestObject;
                }
            }
            return nearestObject;
        }
    }

    protected void Move(Vector3 direction) 
    {
        transform.position += direction.normalized * moveSpeed * Time.fixedDeltaTime;
    }

    protected void Turn(int direction) 
    {
        transform.Rotate(transform.up, turnSpeed * direction * Time.fixedDeltaTime);
    }
    protected void Turn(int direction, float speedModifier)
    {
        transform.Rotate(transform.up, turnSpeed * speedModifier * direction * Time.fixedDeltaTime);
    }
}
