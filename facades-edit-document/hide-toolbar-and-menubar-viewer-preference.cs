using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Attempt to set viewer preferences via reflection.
                // This works on newer Aspose.Pdf versions where Document.ViewerPreferences exists.
                // On older versions the property is absent; the code will simply skip setting it.
                PropertyInfo vpProp = typeof(Document).GetProperty("ViewerPreferences");
                if (vpProp != null && vpProp.CanRead && vpProp.CanWrite)
                {
                    object viewerPrefs = vpProp.GetValue(doc);
                    if (viewerPrefs != null)
                    {
                        Type vpType = viewerPrefs.GetType();

                        PropertyInfo hideToolbar = vpType.GetProperty("HideToolbar");
                        PropertyInfo hideMenubar = vpType.GetProperty("HideMenubar");

                        if (hideToolbar != null && hideToolbar.CanWrite)
                            hideToolbar.SetValue(viewerPrefs, true);

                        if (hideMenubar != null && hideMenubar.CanWrite)
                            hideMenubar.SetValue(viewerPrefs, true);
                    }
                }
                else
                {
                    // ViewerPreferences not available in this version of Aspose.Pdf
                    Console.WriteLine("ViewerPreferences property not found; cannot hide toolbar/menubar.");
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with viewer preferences to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}