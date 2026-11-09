using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "viewerPreferences.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdfPath))
        {
            string pageLayout = "NotSupported";
            string zoom = "NotSupported";

            // ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // Use dynamic access to avoid compile‑time errors on older versions.
            try
            {
                dynamic dynDoc = doc; // bypass compile‑time member check
                var prefs = dynDoc.ViewerPreferences; // may throw RuntimeBinderException

                // Access the properties safely – they are also dynamic.
                pageLayout = prefs.PageLayout?.ToString() ?? "Undefined";
                zoom = prefs.Zoom?.ToString() ?? "Undefined";
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf library version. Using default placeholders.");
            }
            catch (Exception ex)
            {
                // Any other unexpected error while reading preferences.
                Console.WriteLine($"Error retrieving viewer preferences: {ex.Message}");
            }

            var config = new
            {
                PageLayout = pageLayout,
                Zoom = zoom
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Viewer preferences saved to '{outputJsonPath}'.");
        }
    }
}
