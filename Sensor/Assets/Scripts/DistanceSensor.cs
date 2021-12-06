using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NearestObject
{
    public float Distance { get; set; }
    public GameObject DetectedObject { get; set; }
    public NearestObject(float distance, GameObject gameObject)
    {
        Distance = distance;
        DetectedObject = gameObject;
    }
}

public class DistanceSensor : MonoBehaviour
{
    public float SensorDistance { get => sensorDistance; set => sensorDistance = value; }

    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float sensorDistance = 5f;
    [SerializeField] private Vector3 sensorDetectionSize;

    public NearestObject DetectedObject
    {
        get
        {
            if (Physics.BoxCast(transform.position, sensorDetectionSize / 2, transform.forward, out RaycastHit hit, transform.rotation, sensorDistance, detectionLayer.value))
            {
                float hitDistance = (hit.point - transform.position).magnitude;

                return new NearestObject(hitDistance, hit.collider.gameObject);
            }
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        float fullSensorlength = sensorDetectionSize.z / 4 + sensorDistance;
        Vector3 senseCenter  = transform.position + transform.forward * (sensorDetectionSize.z / 2 + sensorDistance) / 2;
        
        if (Physics.BoxCast(transform.position, sensorDetectionSize / 2, transform.forward, out RaycastHit hit, transform.rotation, sensorDistance, detectionLayer.value))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hit.point, 0.5f);
            Gizmos.DrawLine(transform.position, hit.point);
        }
    }
}
