using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Reflection;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "fullScreenMode.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfContentEditor (does not implement IDisposable)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Try to set ViewerPreferences.PageMode = UseNone using reflection/dynamic so the code
        // compiles against older Aspose.Pdf versions that do not expose the ViewerPreferences API.
        try
        {
            // Obtain the Document instance from the editor.
            Document doc = editor.Document;

            // Look for the ViewerPreferences type in the Aspose.Pdf assembly.
            Type viewerPrefType = doc.GetType().Assembly.GetType("Aspose.Pdf.ViewerPreferences");
            if (viewerPrefType != null)
            {
                // Create an instance of ViewerPreferences.
                object viewerPrefInstance = Activator.CreateInstance(viewerPrefType);

                // Get the PageMode property and its enum type.
                PropertyInfo pageModeProp = viewerPrefType.GetProperty("PageMode");
                Type pageModeEnum = pageModeProp.PropertyType;

                // Parse the enum value "UseNone".
                object useNoneValue = Enum.Parse(pageModeEnum, "UseNone");

                // Set the PageMode property.
                pageModeProp.SetValue(viewerPrefInstance, useNoneValue);

                // Assign the ViewerPreferences instance back to the document.
                PropertyInfo docViewerPrefProp = doc.GetType().GetProperty("ViewerPreferences");
                if (docViewerPrefProp != null && docViewerPrefProp.CanWrite)
                {
                    docViewerPrefProp.SetValue(doc, viewerPrefInstance);
                }
                else
                {
                    // If the property is read‑only (newer versions expose it as read‑only), use reflection to set the backing field.
                    FieldInfo backingField = doc.GetType().GetField("_viewerPreferences", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (backingField != null)
                    {
                        backingField.SetValue(doc, viewerPrefInstance);
                    }
                }
            }
            else
            {
                // ViewerPreferences type not found – the current Aspose.Pdf version does not support it.
                // No action needed; the PDF will be saved without this preference.
            }
        }
        catch (Exception ex)
        {
            // Log but do not abort – the operation is optional.
            Console.Error.WriteLine($"Unable to set ViewerPreferences: {ex.Message}");
        }

        // Save the modified PDF.
        editor.Save(outputPath);
        Console.WriteLine($"PDF saved (PageModeUseNone attempted) to '{outputPath}'.");
    }
}
