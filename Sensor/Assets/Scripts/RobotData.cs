using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RobotData", menuName = "Robot/RobotData", order = 1)]
public class RobotData : ScriptableObject
{
    public float MoveSpeed => moveSpeed;
    public float TurnSpeed => turnSpeed;
    public float ObjectSensorDistance => objectSensorDistance;
    public float LineSensorDistance => lineSensorDistance;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float objectSensorDistance;
    [SerializeField] private float lineSensorDistance;
    [SerializeField] private Material material;


    public void SetValues(Robot robot, bool changeMat = true)
    {
        robot.MoveSpeed = moveSpeed;
        robot.TurnSpeed = turnSpeed;

        robot.DistanceSensors.ForEach(x => x.SensorDistance = ObjectSensorDistance);
        robot.LineSensors.ForEach(x => x.SensorDistance = ObjectSensorDistance);


        if(changeMat && robot.TryGetComponent(out MeshRenderer meshRenderer))
        {
            var tempMat = meshRenderer.materials;
            tempMat[2]  = material;
            meshRenderer.materials = tempMat;
        }
    }
}
