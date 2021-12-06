using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Message { Infect }
public class CommunicationDevice : MonoBehaviour
{
    public delegate void MessageDelegate(Robot sender, Message message);
    public event MessageDelegate OnMessageReceived;
    public void SendMessage(Robot sender, Robot receiver, Message message)
    {
        lastSender = sender; //for gizmos
        lastReceiver = receiver; //for gizmos
        drawTimeCounter = 0; //for gizmos

        receiver.CommunicationDevice.ReceiveMessage(sender, message);
    }

    public void ReceiveMessage(Robot sender, Message message)
    {
        OnMessageReceived?.Invoke(sender, message);
    }

    private Robot lastSender;//for gizmos
    private Robot lastReceiver;//for gizmos
    private float drawTime = 2f;//for gizmos
    private float drawTimeCounter = 0;//for gizmos
    private void OnDrawGizmos()
    {
        if (lastReceiver == null || lastSender == null) return;
        if (drawTimeCounter > drawTime) return;

        drawTimeCounter += 0.025f;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(lastSender.transform.position, 0.5f);
        Gizmos.DrawLine(lastSender.transform.position, lastReceiver.transform.position);
        Gizmos.DrawWireSphere(lastReceiver.transform.position, 0.5f);
    }
}
