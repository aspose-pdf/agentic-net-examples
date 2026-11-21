using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "continuous_view.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Try to configure viewer preferences via reflection – this works with newer
            // Aspose.Pdf versions that expose the ViewerPreferences property and safely
            // degrades when the property is absent (e.g., older library versions).
            PropertyInfo viewerPrefsProp = typeof(Document).GetProperty("ViewerPreferences");
            if (viewerPrefsProp != null)
            {
                object viewerPrefs = viewerPrefsProp.GetValue(doc);
                if (viewerPrefs != null)
                {
                    // Set PageLayout = OneColumn (continuous scrolling)
                    PropertyInfo pageLayoutProp = viewerPrefs.GetType().GetProperty("PageLayout");
                    if (pageLayoutProp != null && pageLayoutProp.CanWrite)
                    {
                        pageLayoutProp.SetValue(viewerPrefs, PageLayout.OneColumn);
                    }

                    // Optional: keep default page mode (UseNone)
                    PropertyInfo pageModeProp = viewerPrefs.GetType().GetProperty("PageMode");
                    if (pageModeProp != null && pageModeProp.CanWrite)
                    {
                        pageModeProp.SetValue(viewerPrefs, PageMode.UseNone);
                    }
                }
            }
            else
            {
                Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf version. The PDF will be saved without explicit viewer preferences.");
            }

            // Save the (potentially) modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with continuous view mode (if supported): '{outputPath}'.");
    }
}
