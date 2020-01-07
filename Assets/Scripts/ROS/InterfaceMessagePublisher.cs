using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using System.Text;
using System;

public class InterfaceMessagePublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage>
{
    /// <summary>
    ///  Start method of InterfaceMessage.
    /// Starts a coroutine to initialize the publisher after 1 second to prevent race conditions.
    /// </summary>
    protected override void Start()
    {
        StartCoroutine(StartPublisher(1.0f));
    }
    /// <summary>
    /// Starts the publisher.
    /// </summary>
    /// <returns>The publisher.</returns>
    /// <param name="waitTime">Wait time.</param>
    private IEnumerator StartPublisher(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            break;
        }
    }

    /// <summary>
    /// Publishs the mock motor status message.
    /// </summary>
    /// <param name="message">message</param>
    private void PublishMessage(RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage message)
    {
        Publish(message);
    }

    private void PublishDemoMessage()
    {
        RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage tmpMessage = new RosSharp.RosBridgeClient.Messages.Roboy.InterfaceMessage();
        tmpMessage.id = 1;
        byte[] test = new byte[5];
        test[0] = 1;
        test[1] = 1;
        test[2] = 1;
        test[3] = 0;
        test[4] = 1;
        tmpMessage.data = Encoding.UTF8.GetBytes("TestNachricht");
        PublishMessage(tmpMessage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Publishing...");
            PublishDemoMessage();
        }
    }
}
