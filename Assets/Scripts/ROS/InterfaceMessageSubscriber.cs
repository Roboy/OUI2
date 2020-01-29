﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;

public class InterfaceMessageSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Standard.String>
{
    /// <summary>
    /// Holds a queue of messages to be read one after the other from the manager.
    /// </summary>
    private Queue<JSON_message> interfaceMessageQueue;
    /// <summary>
    /// Start this instance.
    /// </summary>
    protected override void Start()
    {
        interfaceMessageQueue = new Queue<JSON_message>();
        StartCoroutine(startSubscriber(1.0f));
    }

    /// <summary>
    /// Enqueues the interface message queue.
    /// </summary>
    /// <param name="msg">The interface Message.</param>
    public void EnqueueInterfaceMessage(JSON_message msg)
    {
        interfaceMessageQueue.Enqueue(msg);
    }

    /// <summary>
    /// Dequeues the interface message queue.
    /// </summary>
    /// <returns>The interface message.</returns>
    public JSON_message DequeueInterfaceMessage()
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
    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Standard.String message)
    {
        Debug.Log("Received Message");
        JSON_message msg = JsonUtility.FromJson<JSON_message>(message.data);
        EnqueueInterfaceMessage(msg);
    }
}
