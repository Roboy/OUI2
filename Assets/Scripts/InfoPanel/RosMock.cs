using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    private float temperature;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            float tempDiff = UnityEngine.Random.Range(0f, 5.0f);
            temperature += tempDiff;
            //send the message to the graph
            CreateTemperatureData(1, temperature, 0, 0, 1);
            // Same message on inspector widget id = 3
            CreateTemperatureData(3, temperature, 0, 0, 1);
            if (temperature > 40 && temperature - tempDiff <= 40)
            {
                // Send a message that the temperature is critically high
                CreateTemperatureWarningData(2, 0, 6, 1, 50, "Temperature is critically high!");
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            float tempDiff = UnityEngine.Random.Range(0f, 7.0f);
            temperature -= tempDiff;
            //send the message to the graph
            CreateTemperatureData(1, temperature, 0, 0, 1);
            // Same message on inspector widget id = 3
            CreateTemperatureData(3, temperature, 0, 0, 1);
            if (temperature < 0 && temperature + tempDiff >= 0)
            {
                // Send a message that the temperature is critically high
                CreateTemperatureWarningData(2, 0, 6, 1, 50, "Temperature is critically low!");
            }
        }
    }

    private void CreateTemperatureData(int id, float datapoint, int pos, int detailedPanelPos, int color)
    {
        byte[] data = new byte[(sizeof(float) + 3 * sizeof(int))];
        int offset = 0;

        offset = AppendData(data, offset, datapoint);
        offset = AppendData(data, offset, pos);
        offset = AppendData(data, offset, detailedPanelPos);
        offset = AppendData(data, offset, color);

        RosMock.Instance.EnqueueNewMessage(new RosMessage(id, data));
    }

    private void CreateTemperatureWarningData(int id, int pos, float duration, int color, int fontSize, string msg)
    {
        byte[] data = new byte[(sizeof(float) + 3 * sizeof(int) + msg.Length * sizeof(char) + 1)];
        int offset = 0;

        offset = AppendData(data, offset, pos);
        offset = AppendData(data, offset, duration);
        offset = AppendData(data, offset, color);
        offset = AppendData(data, offset, fontSize);
        offset = AppendData(data, offset, msg);

        RosMock.Instance.EnqueueNewMessage(new RosMessage(id, data));
    }

    private int AppendData(byte[] data, int offset, int i)
    {
        byte[] newData = BitConverter.GetBytes(i);
        Buffer.BlockCopy(newData, 0, data, offset, newData.Length);
        return offset + sizeof(int);
    }

    private int AppendData(byte[] data, int offset, float i)
    {
        byte[] newData = BitConverter.GetBytes(i);
        Buffer.BlockCopy(newData, 0, data, offset, newData.Length);
        return offset + sizeof(float);
    }

    private int AppendData(byte[] data, int offset, string i)
    {
        MemoryStream dataStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(dataStream);
        binaryWriter.Write(i);
        byte[] newData = dataStream.ToArray();
        Buffer.BlockCopy(newData, 0, data, offset, newData.Length);
        return offset + newData.Length;

        /*data[offset++] = BitConverter.GetBytes(i.Length)[0];
        print(data[offset - 1]);
        foreach (char c in i) {
            offset = AppendData(data, offset, c);
        }
        return offset;*/
    }

    private int AppendData(byte[] data, int offset, char i)
    {
        byte[] newData = BitConverter.GetBytes(i);
        Buffer.BlockCopy(newData, 0, data, offset, newData.Length);
        return offset + sizeof(char);
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
