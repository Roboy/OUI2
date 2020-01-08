using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;

public class InterfaceMessageSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage>
{
    /// <summary>
    /// Holds a queue of messages to be read one after the other from the manager.
    /// </summary>
    private Queue<RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage> interfaceMessageQueue;
    /// <summary>
    /// Start this instance.
    /// </summary>
    protected override void Start()
    {
        interfaceMessageQueue = new Queue<RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage>();
        StartCoroutine(startSubscriber(1.0f));
    }

    /// <summary>
    /// Enqueues the interface message queue.
    /// </summary>
    /// <param name="msg">The interface Message.</param>
    public void EnqueueInterfaceMessage(RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage msg)
    {
        interfaceMessageQueue.Enqueue(msg);
    }

    /// <summary>
    /// Dequeues the interface message queue.
    /// </summary>
    /// <returns>The interface message.</returns>
    public RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage DequeueInterfaceMessage()
    {
        return interfaceMessageQueue.Dequeue();
    }

    /// <summary>
    /// Counts the number of objects in the queue.
    /// </summary>
    /// <returns>The queue count.</returns>
    public int MessageQueueCount()
    {
        return interfaceMessageQueue.Count;
    }

    public bool IsEmpty()
    {
        return interfaceMessageQueue.Count == 0;
    }

    /// <summary>
    /// Starts the subscriber.
    /// </summary>
    /// <returns>The subscriber.</returns>
    /// <param name="waitTime">Wait time.</param>
    private IEnumerator startSubscriber(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            break;
        }
    }

    /// <summary>
    /// Receives the message by enqueueing the queue.
    /// </summary>
    /// <param name="message">Message.</param>
    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage message)
    {
        // Debug.Log("Received Message: ID=" + message.id + " | Data=" + System.Text.Encoding.UTF8.GetString(message.data));
        EnqueueInterfaceMessage(message);
    }
}
