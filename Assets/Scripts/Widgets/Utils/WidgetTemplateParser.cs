using System.Collections.Generic;
using UnityEngine;

public static class WidgetTemplateParser
{
    public static List<WidgetContext> ParseAllWidgetTemplates()
    {
        List<WidgetContext> widgetContexts = new List<WidgetContext>();

        TextAsset[] widgetTemplates = Resources.LoadAll<TextAsset>("JsonTemplates");
        foreach (TextAsset widgetTemplate in widgetTemplates)
        {
            WidgetContext parsedWidgetContext = ParseWidgetTemplate(widgetTemplate);

            if (parsedWidgetContext == null)
            {
                continue;
            }

            widgetContexts.Add(ParseWidgetTemplate(widgetTemplate));
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
