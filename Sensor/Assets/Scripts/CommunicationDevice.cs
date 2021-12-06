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
        receiver.CommunicationDevice.ReceiveMessage(sender, message);
    }

    public void ReceiveMessage(Robot sender, Message message)
    {
        OnMessageReceived?.Invoke(sender, message);
    }
}
