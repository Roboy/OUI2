using System.Collections.Generic;
using UnityEngine;

public static class JsonReceiver
{
    public static List<WidgetContext> ParseAllWidgetTemplates()
    {
        List<WidgetContext> widgetContexts = new List<WidgetContext>();

        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("JsonTemplates");
        foreach (TextAsset asset in textAssets)
        {
            WidgetContext parsedWidgetContext = ParseWidgetTemplate(asset);

            if (parsedWidgetContext == null)
            {
                continue;
            }

            widgetContexts.Add(ParseWidgetTemplate(asset));
        }

        return widgetContexts;
    }

    private static WidgetContext ParseWidgetTemplate(TextAsset asset)
    {
        WidgetContext parsedContext = JsonUtility.FromJson<WidgetContext>(asset.text);
        if (parsedContext == null)
        {
            Debug.LogWarning("Json " + asset.text + " is faulty");
            return null;
        }

        return parsedContext;
    }
}



/*
 
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


 */
