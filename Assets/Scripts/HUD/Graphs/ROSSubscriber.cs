using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROSSubscriber : Singleton<ROSSubscriber>
{
    struct message
    {
        int id;
        byte[] data;
    }

    Queue<message> receivedMessages;

    // Start is called before the first frame update
    void Start()
    {
        receivedMessages = new Queue<message>();
    }

    // Update is called once per frame
    void Update()
    {
        ReceiveMessages();
    }

    private void ReceiveMessages()
    {

    }
}
