using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ReceivedJsonData d = new ReceivedJsonData(0, "temperature", BitConverter.GetBytes(27), 0);
        //print(JsonUtility.ToJson(d));
        //string s = Resources.Load<TextAsset>("TestTemplate").text;
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("JsonTemplates");
        foreach (TextAsset asset in textAssets)
        {
            print(asset.text);
            ReceivedJsonData r = JsonUtility.FromJson<ReceivedJsonData>(asset.text);
            if (r == null)
            {
                print("Json " + asset.text);
                continue;
            }
            print(r.pos);
            
            // Create a new Bytearray
            Byte[] bs = BitConverter.GetBytes(2);
            Byte[] bs2 = BitConverter.GetBytes(13);
            Byte[] bs3 = new byte[8];
            Array.Copy(bs, 0, bs3, 0, 4);
            Array.Copy(bs2, 0, bs3, 4, 4);
            
            // Convert Bytearray to two ints
            int msg_type = BitConverter.ToInt32(bs3, 0);
            print(msg_type);
            // 
            if (msg_type == (int)LiveDataType.Graph)
            {
                int y = BitConverter.ToInt32(bs3, 4);
                print(y);
            }

        }
        //print(s);
        
        
    }
}

enum LiveDataType
{
    ErrorMsg, Msg, Graph
}
