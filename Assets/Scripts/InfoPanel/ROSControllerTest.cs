using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class subscribes to the log message topics and queues messages, so the LogText can pull new messages.
/// In a push architecture, this class could not funtion correctly. RosSharp classes behave weird sometimes. Pull architecture works though.
/// For every log level, there is a subclass to handle that topic.
/// </summary>
public class ROSManager : Singleton<ROSManager>
{    
    public string topic;

    // This messageQueue is filled with incoming messages
    private Queue<RosSharp.RosBridgeClient.Message> messageQueue;
    
    public void EnqueueMessage(RosSharp.RosBridgeClient.Message msg)
    {
        messageQueue.Enqueue(msg);
    }
    // Start is called before the first frame update
    void Start()
    {
        messageQueue = new Queue<Message>();

        ROSSubscriber infoSubscriber = gameObject.AddComponent<ROSSubscriber>();
        infoSubscriber.Topic = topic;
    }

    /// <summary>
    /// Subclass for info messages
    /// </summary>
    private class ROSSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification>
    {
        /// <summary>
        /// Holds the currently received data for other objects to read
        /// </summary>
        private string messageData;

        /// <summary>
        /// Start method of TestSubscriber.
        /// Starts a coroutine to initialize the subscriber after 1 second to prevent race conditions.
        /// </summary>
        protected override void Start()
        {
            StartCoroutine(startSubscriber(1.0f));
        }

        /// <summary>
        /// Initializes the subscriber.
        /// </summary>
        /// <param name="waitTime"> defines the time, after that subscriber is initialized.</param>
        /// <returns></returns>
        public IEnumerator startSubscriber(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                base.Start();
                break;
            }
        }

        /// <summary>
        /// This handler is called, whenever a message on the subscribed topic is received.
        /// </summary>
        /// <param name="message"> is the received message.</param>
        protected override void ReceiveMessage(InfoNotification message)
        {
            // Split operator and roboy log with code 0 for operator log, 1 for roboy log
            if (message.code == 0)
                ROSManager.Instance.EnqueueMessage(message);
        }
    }
}