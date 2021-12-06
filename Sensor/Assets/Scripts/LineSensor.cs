using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineSendorSide { None, Left, Right }
public class LineSensor : MonoBehaviour
{
    public LineSendorSide LineSendorSide => lineSendorSide;

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
}
