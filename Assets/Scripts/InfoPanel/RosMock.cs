using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosMock : Singleton<RosMock>
{
    private Queue<RosMessage> rosMessageQueue;
    private RosMockSubscriber subscriber;

    // Start is called before the first frame update
    void Start()
    {
        rosMessageQueue = new Queue<RosMessage>();
        subscriber = gameObject.AddComponent<RosMockSubscriber>() as RosMockSubscriber;
    }

    public void EnqueueNewMessage(RosMessage message)
    {
        Debug.Log("New Ros message received");

        rosMessageQueue.Enqueue(message);        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public bool HasNewMessage()
    {
        if (rosMessageQueue.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public RosMessage DequeueMessage()
    {
        return rosMessageQueue.Dequeue();   
    }
}

public class RosMockSubscriber : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            int id = 1;

            float datapoint = UnityEngine.Random.Range(5.0f, 15.0f);
            int pos = 1;
            int detailedPosition = 1;
            int color = 1;

            byte[] data = new byte[(sizeof(float) + sizeof(int) + sizeof(int) + sizeof(int))];

            Buffer.BlockCopy(BitConverter.GetBytes(datapoint),          0, data, 0,                                                 BitConverter.GetBytes(datapoint).Length);
            Buffer.BlockCopy(BitConverter.GetBytes(pos),                0, data, sizeof(float),                                     BitConverter.GetBytes(pos).Length);
            Buffer.BlockCopy(BitConverter.GetBytes(detailedPosition),   0, data, sizeof(float) + sizeof(int),                       BitConverter.GetBytes(detailedPosition).Length);
            Buffer.BlockCopy(BitConverter.GetBytes(color),              0, data, sizeof(float) + sizeof(int) + sizeof(int),         BitConverter.GetBytes(color).Length);

            RosMock.Instance.EnqueueNewMessage(new RosMessage(id, data));
                        
            // Same message on inspector widget id = 3
            RosMock.Instance.EnqueueNewMessage(new RosMessage(3, data));
        }
    }
}

public class RosMessage
{
    public int id;
    public byte[] data;

    public RosMessage(int id, byte[] data)
    {
        this.id = id;
        this.data = data;
    }
}
