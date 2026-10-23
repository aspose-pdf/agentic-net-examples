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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // ---- Remove document‑level JavaScript actions ----
                ClearJavascriptActions(doc.Actions);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Remove page‑level JavaScript actions
                    ClearJavascriptActions(page.Actions);

                    // Iterate over annotations in reverse order to allow safe removal if needed
                    for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                    {
                        Annotation annotation = page.Annotations[annIndex];

                        // Remove any JavaScript actions attached to the annotation via its Actions collection
                        ClearJavascriptActions(annotation.Actions);
                    }
                }

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"JavaScript actions removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Clears all properties of type <see cref="JavascriptAction"/> from the supplied actions collection.
    /// The method uses reflection so it works across Aspose.Pdf versions where the exact set of
    /// JavaScript‑related properties may differ.
    /// </summary>
    private static void ClearJavascriptActions(object actionsCollection)
    {
        if (actionsCollection == null)
            return;

        // Iterate over all public instance properties of the collection
        foreach (PropertyInfo prop in actionsCollection.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            // If the property type is JavascriptAction (the correct class name in Aspose.Pdf) and it can be written to,
            // set it to null – effectively removing the script.
            if (prop.PropertyType == typeof(JavascriptAction) && prop.CanWrite)
            {
                prop.SetValue(actionsCollection, null);
            }
        }
    }
}
