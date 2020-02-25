using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public static class TemplateParser
    {
        public static List<Context> ParseAllWidgetTemplates()
        {
            List<Context> widgetContexts = new List<Context>();

            TextAsset[] widgetTemplates = Resources.LoadAll<TextAsset>("JsonTemplates");
            foreach (TextAsset widgetTemplate in widgetTemplates)
            {
                Context parsedWidgetContext = ParseWidgetTemplate(widgetTemplate);

                if (parsedWidgetContext == null)
                {
                    continue;
                }

                widgetContexts.Add(ParseWidgetTemplate(widgetTemplate));
            }

            return widgetContexts;
        }

        private static Context ParseWidgetTemplate(TextAsset asset)
        {
            Context parsedContext = JsonUtility.FromJson<Context>(asset.text);
            if (parsedContext == null)
            {
                Debug.LogWarning("Json " + asset.text + " is faulty");
                return null;
            }

            return parsedContext;
        }
    }
}
