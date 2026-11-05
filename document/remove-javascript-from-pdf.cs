using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---- Remove document‑level JavaScript ----
            // Document.JavaScript is read‑only; clear actions via reflection.
            ClearJavascriptActions(doc.Actions);

            // Some PDFs have an OpenAction that executes JavaScript.
            // Setting it to null disables the action.
            doc.OpenAction = null;

            // ---- Iterate through all pages ----
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Remove page‑level actions (e.g., OnOpen, OnClose) that may contain JavaScript.
                if (page.Actions != null)
                {
                    ClearJavascriptActions(page.Actions);
                }

                // Remove JavaScript actions attached to annotations.
                foreach (Annotation annot in page.Annotations)
                {
                    // LinkAnnotation may contain a JavaScript action.
                    if (annot is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        link.Action = null;
                    }

                    // WidgetAnnotation (form fields) can have actions; clear them via reflection.
                    if (annot is WidgetAnnotation widget && widget.Actions != null)
                    {
                        ClearJavascriptActions(widget.Actions);
                    }
                }
            }

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"All JavaScript removed. Saved to '{outputPath}'.");
    }

    /// <summary>
    /// Clears any JavascriptAction properties from the given action collection using reflection.
    /// </summary>
    private static void ClearJavascriptActions(object actionCollection)
    {
        if (actionCollection == null) return;
        PropertyInfo[] props = actionCollection.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            // Only consider writable properties of type JavascriptAction
            if (prop.CanWrite && prop.PropertyType == typeof(JavascriptAction))
            {
                prop.SetValue(actionCollection, null);
            }
        }
    }
}
