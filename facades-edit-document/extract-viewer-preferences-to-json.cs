using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException when using dynamic

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "viewerPreferences.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Use dynamic access to guard against older Aspose.Pdf versions that lack ViewerPreferences
            string pageLayoutStr = "Unknown";
            int zoom = 0;

            try
            {
                dynamic docDyn = pdfDocument;
                var prefs = docDyn.ViewerPreferences; // May throw RuntimeBinderException on older versions
                // PageLayout is an enum; convert to string safely
                pageLayoutStr = prefs.PageLayout != null ? prefs.PageLayout.ToString() : "Unknown";
                // Zoom is typically an int (percentage). Cast to int safely.
                zoom = prefs.Zoom != null ? (int)prefs.Zoom : 0;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Using default values.");
            }
            catch (Exception ex)
            {
                // Any other unexpected error while reading preferences
                Console.WriteLine($"Unexpected error while accessing ViewerPreferences: {ex.Message}");
            }

            // Prepare a simple configuration object
            var config = new
            {
                PageLayout = pageLayoutStr,
                Zoom = zoom
            };

            // Serialize to indented JSON
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Viewer preferences saved to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
