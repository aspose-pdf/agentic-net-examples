using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class RemoveJavaScript
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ----- Remove document‑level JavaScript actions -----
            // Document may expose several action properties (OnOpen, OnClose, etc.).
            // Iterate over them via reflection and clear any JavascriptAction.
            ClearJavascriptActions(doc.Actions);

            // Remove OpenAction if it is a JavaScript action
            if (doc.OpenAction is JavascriptAction)
                doc.OpenAction = null;

            // ----- Iterate through all pages -----
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Remove page‑level JavaScript actions (OnOpen, OnClose, etc.)
                ClearJavascriptActions(page.Actions);

                // Remove JavaScript from annotations (iterate backwards to allow removal if needed)
                for (int annotIndex = page.Annotations.Count; annotIndex >= 1; annotIndex--)
                {
                    Annotation annot = page.Annotations[annotIndex];

                    // Only LinkAnnotation (and a few others) expose an Action property.
                    // Guard with a type‑check before accessing.
                    if (annot is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        link.Action = null;
                    }
                    // WidgetAnnotation (form fields) expose an Actions collection.
                    else if (annot is WidgetAnnotation widget && widget.Actions != null)
                    {
                        ClearJavascriptActions(widget.Actions);
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript removed. Output saved to '{outputPath}'.");
    }

    /// <summary>
    /// Clears any JavascriptAction found in the supplied action collection (DocumentActionCollection, PageActionCollection, or AnnotationActions).
    /// </summary>
    private static void ClearJavascriptActions(object actionsCollection)
    {
        if (actionsCollection == null) return;

        // Use reflection to enumerate all public instance properties of the collection.
        PropertyInfo[] props = actionsCollection.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in props)
        {
            // Only writable properties are relevant.
            if (!prop.CanWrite) continue;

            // If the property value is a JavascriptAction, set it to null.
            if (prop.GetValue(actionsCollection) is JavascriptAction)
            {
                prop.SetValue(actionsCollection, null);
            }
            // Some collections (e.g., WidgetAnnotation.Actions) expose nested action objects like OnMouseEnter, OnMouseExit, etc.
            // Those nested objects are also of type JavascriptAction, so the same logic applies.
        }
    }
}
