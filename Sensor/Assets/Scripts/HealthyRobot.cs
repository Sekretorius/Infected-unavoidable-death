using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyRobot : Robot
{
    private bool isTurning = false;
    private int turnDirection = 1;

    private int randomRange = 10;
    private void FixedUpdate()
    {
        if (IsLineDetected.IsDetected)
        {
            turnDirection = IsLineDetected.LineSendorSide == LineSendorSide.Left ? 1 : -1;
            Turn(turnDirection);
            return;
        }


        if(NearestObject.Distance > 1.5f) 
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
        else //obstictle avoidence
        {
            isTurning = true;
            Turn(turnDirection);
        }
    }
}
