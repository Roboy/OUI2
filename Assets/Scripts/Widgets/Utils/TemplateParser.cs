using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public static class TemplateParser
    {
        public static List<RosJsonMessage> ParseAllWidgetTemplates()
        {
            List<RosJsonMessage> widgetContexts = new List<RosJsonMessage>();

            TextAsset[] widgetTemplates = Resources.LoadAll<TextAsset>("JsonTemplates");
            foreach (TextAsset widgetTemplate in widgetTemplates)
            {
                RosJsonMessage parsedWidgetContext = ParseWidgetTemplate(widgetTemplate);

                if (parsedWidgetContext == null)
                {
                    continue;
                }

                widgetContexts.Add(ParseWidgetTemplate(widgetTemplate));
            }

            return widgetContexts;
        }

        private static RosJsonMessage ParseWidgetTemplate(TextAsset asset)
        {
            RosJsonMessage parsedContext = JsonUtility.FromJson<RosJsonMessage>(asset.text);
            if (parsedContext == null)
            {
                Debug.LogWarning("Json " + asset.text + " is faulty");
                return null;
            }

            return parsedContext;
        }
    }
}
