using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using System.Text;
using System;
using System.IO;

namespace Widgets
{
    public class RosPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.String>
    {
        private float temperature = 20;

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
        private void PublishMessage(RosSharp.RosBridgeClient.Messages.Standard.String message)
        {
            Publish(message);
        }

        private void PublishDemoMessage()
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            RosJsonMessage demoMessage = RosJsonMessage.CreateGraphMessage(1, UnityEngine.Random.Range(20.0f, 25.0f), cur_time, new byte[] { 255, 255, 255, 255 });
            string jsonString = JsonUtility.ToJson(demoMessage);
            RosSharp.RosBridgeClient.Messages.Standard.String tmpMessage = new RosSharp.RosBridgeClient.Messages.Standard.String(jsonString);
            PublishMessage(tmpMessage);


            RosJsonMessage demoMessageInspector = RosJsonMessage.CreateGraphMessage(3, UnityEngine.Random.Range(20.0f, 25.0f), cur_time, new byte[] { 255, 255, 255, 255 });
            string jsonStringInspector = JsonUtility.ToJson(demoMessageInspector);
            RosSharp.RosBridgeClient.Messages.Standard.String tmpMessageInspector = new RosSharp.RosBridgeClient.Messages.Standard.String(jsonStringInspector);
            PublishMessage(tmpMessageInspector);

            WriteMessageToFile("demoMessage", jsonString);
            WriteMessageToFile("demoMessage", jsonStringInspector);



            /*
            tmpMessage.id = 2;
            tmpMessage.data = Encoding.UTF8.GetBytes("TestNachricht");
            PublishMessage(tmpMessage);
            */
        }

        private bool WriteMessageToFile(string fileName, string json)
        {
            string filePath = Application.persistentDataPath + fileName + ".json";

            print("Saved demoMessage at " + filePath);

            File.WriteAllText(filePath, json);

            return true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                PublishDemoMessage();
            }

            /*
            if (Input.GetKeyDown(KeyCode.M))
            {
                float tempDiff = UnityEngine.Random.Range(0f, 5.0f);
                temperature += tempDiff;
                //send the message to the graph
                PublishMessage(CreateTemperatureData(1, temperature, 0, 0, GetColor()));
                // Same message on inspector widget id = 3
                PublishMessage(CreateTemperatureData(3, temperature, 0, 0, GetColor()));
                if (temperature > 40 && temperature - tempDiff <= 40)
                {
                    // Send a message that the temperature is critically high
                    PublishMessage(CreateTemperatureWarningData(2, 0, 3, GetColor(), 50, "Temperature is critically high!"));
                }
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                float tempDiff = UnityEngine.Random.Range(0f, 7.0f);
                temperature -= tempDiff;
                string color;
                //send the message to the graph
                PublishMessage(CreateTemperatureData(1, temperature, 0, 0, GetColor()));
                // Same message on inspector widget id = 3
                PublishMessage(CreateTemperatureData(3, temperature, 0, 0, GetColor()));
                if (temperature < 0 && temperature + tempDiff >= 0)
                {
                    // Send a message that the temperature is critically high
                    PublishMessage(CreateTemperatureWarningData(2, 0, 3, GetColor(), 50, "Temperature is critically low!"));
                }
            }    
            */
        }

        /*
        private byte[] GetColor()
        {
            if (temperature > 30)
            {
                return new byte[] { 0xFF, 0, 0, 0xFF };
            }
            else if (temperature < 10)
            {
                return new byte[] { 0, 0, 0xFF, 0xFF };
            }
            else
            {
                return new byte[] { 0xFF, 0xFE, 0x01, 0xFF };
            }
        }

        private RosSharp.RosBridgeClient.Messages.Standard.String CreateTemperatureData(int id, float datapoint, int pos, int detailedPanelPos, byte[] color)
        {
            byte[] data = new byte[(sizeof(float) + 3 * sizeof(int))];
            int offset = 0;

            offset = AppendData(data, offset, datapoint);
            offset = AppendData(data, offset, pos);
            offset = AppendData(data, offset, detailedPanelPos);
            offset = AppendData(data, offset, color);

            RosSharp.RosBridgeClient.Messages.Standard.String rosMessage = new RosSharp.RosBridgeClient.Messages.Standard.String();

            rosMessage.id = id;
            rosMessage.data = data;

            return rosMessage;
        }

        private RosSharp.RosBridgeClient.Messages.Standard.String CreateTemperatureWarningData(int id, int pos, float duration, byte[] color, int fontSize, string msg)
        {
            byte[] data = new byte[(sizeof(float) + 3 * sizeof(int) + msg.Length * sizeof(char) + 1)];
            int offset = 0;

            offset = AppendData(data, offset, pos);
            offset = AppendData(data, offset, duration);
            offset = AppendData(data, offset, color);
            offset = AppendData(data, offset, fontSize);
            offset = AppendData(data, offset, msg);

            RosSharp.RosBridgeClient.Messages.Standard.String rosMessage = new RosSharp.RosBridgeClient.Messages.Standard.String();

            rosMessage.id = id;
            rosMessage.data = data;

            return rosMessage;
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

        private int AppendData(byte[] data, int offset, byte[] i)
        {
            byte[] newData = i;
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
            return offset;
        }

        private int AppendData(byte[] data, int offset, char i)
        {
            byte[] newData = BitConverter.GetBytes(i);
            Buffer.BlockCopy(newData, 0, data, offset, newData.Length);
            return offset + sizeof(char);
        }

        */
    }
}