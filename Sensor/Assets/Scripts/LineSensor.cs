using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineSendorSide { None, Left, Right }
public class LineSensor : MonoBehaviour
{
    public LineSendorSide LineSendorSide => lineSendorSide;
    public float SensorDistance { get => sensorDistance; set => sensorDistance = value; }

    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float sensorDistance = 5f;

    [SerializeField] private LineSendorSide lineSendorSide;

    public bool IsBlack
    {
        get
        {
            return Physics.Raycast(transform.position, transform.forward, sensorDistance, detectionLayer.value);
        }
    }

    private void OnDrawGizmos()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, sensorDistance, detectionLayer.value))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(raycastHit.point, Vector3.one);
        }
    }
}
