using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

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

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Try to set viewer preferences (PageLayout = OneColumn, Zoom = 100%)
        // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
        // We use reflection to keep the code compilable with older versions as well.
        PropertyInfo viewerPrefProp = typeof(Document).GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
        if (viewerPrefProp != null)
        {
            object viewerPrefs = viewerPrefProp.GetValue(pdfDoc);
            if (viewerPrefs != null)
            {
                // Set PageLayout = OneColumn (continuous scrolling)
                PropertyInfo pageLayoutProp = viewerPrefs.GetType().GetProperty("PageLayout", BindingFlags.Public | BindingFlags.Instance);
                if (pageLayoutProp != null && pageLayoutProp.CanWrite)
                {
                    pageLayoutProp.SetValue(viewerPrefs, PageLayout.OneColumn);
                }

                // Set Zoom = 100 (default zoom 100%)
                PropertyInfo zoomProp = viewerPrefs.GetType().GetProperty("Zoom", BindingFlags.Public | BindingFlags.Instance);
                if (zoomProp != null && zoomProp.CanWrite)
                {
                    zoomProp.SetValue(viewerPrefs, 100);
                }
            }
        }
        else
        {
            Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf version. Skipping viewer preference configuration.");
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Viewer preferences applied (if supported) and saved to '{outputPath}'.");
    }
}
