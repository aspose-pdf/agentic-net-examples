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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Attempt to set viewer preferences using reflection so the code compiles
                // against older Aspose.Pdf versions that do not expose ViewerPreferences.
                SetViewerPreferencesIfSupported(pdfDoc);

                // Save the modified PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Viewer preferences applied (if supported) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the PDF to open with the thumbnails pane visible and the default zoom to fit width.
    /// The implementation uses reflection to remain compatible with older Aspose.Pdf versions
    /// that do not contain the ViewerPreferences property or related enums.
    /// </summary>
    /// <param name="doc">The Aspose.Pdf Document instance.</param>
    private static void SetViewerPreferencesIfSupported(Document doc)
    {
        // Get the ViewerPreferences property (if it exists)
        PropertyInfo viewerPrefProp = doc.GetType().GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
        if (viewerPrefProp == null)
        {
            // The current Aspose.Pdf version does not support ViewerPreferences.
            Console.WriteLine("ViewerPreferences property is not available in this Aspose.Pdf version.");
            return;
        }

        // Retrieve the ViewerPreferences object
        object viewerPrefs = viewerPrefProp.GetValue(doc);
        if (viewerPrefs == null)
            return;

        // PageMode property
        PropertyInfo pageModeProp = viewerPrefs.GetType().GetProperty("PageMode", BindingFlags.Public | BindingFlags.Instance);
        // Zoom property
        PropertyInfo zoomProp = viewerPrefs.GetType().GetProperty("Zoom", BindingFlags.Public | BindingFlags.Instance);

        if (pageModeProp == null || zoomProp == null)
            return; // Unexpected shape of ViewerPreferences; exit gracefully.

        // Resolve the enum types for PageMode and Zoom
        Type pageModeEnum = pageModeProp.PropertyType;
        Type zoomEnum = zoomProp.PropertyType;

        try
        {
            // Parse enum values by name ("UseThumbs" and "FitWidth")
            object useThumbs = Enum.Parse(pageModeEnum, "UseThumbs");
            object fitWidth = Enum.Parse(zoomEnum, "FitWidth");

            // Assign the values
            pageModeProp.SetValue(viewerPrefs, useThumbs);
            zoomProp.SetValue(viewerPrefs, fitWidth);
        }
        catch (ArgumentException)
        {
            // Enum names not found – likely an older version where the enum members differ.
            Console.WriteLine("Unable to set specific ViewerPreferences values – enum members not found.");
        }
    }
}
