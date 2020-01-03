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
            WidgetContext widgetContext = ParseWidgetTemplate(asset);

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

    private WidgetContext ParseWidgetTemplate(TextAsset asset)
    {
        print(asset.text);
        WidgetContext r = JsonUtility.FromJson<WidgetContext>(asset.text);
        if (r == null)
        {
            Debug.LogWarning("Json " + asset.text + " is faulty");
            return null;
        }
        print(r.pos);

        return r;
    }
}

enum LiveDataType
{
    ErrorMsg, Msg, Graph
}
