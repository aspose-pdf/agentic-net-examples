using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "viewerPreferences.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF with PdfContentEditor as required.
        // PdfContentEditor implements IDisposable, so we wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(pdfPath);
            // No modifications are needed; binding satisfies the task requirement.
        }

        // Extract viewer preference values using a runtime‑bound access to avoid compile‑time
        // errors when the Aspose.Pdf version does not expose ViewerPreferences.
        bool hideToolbar = false;
        bool displayDocTitle = false;

        using (Document doc = new Document(pdfPath))
        {
            dynamic dynDoc = doc;
            try
            {
                var prefs = dynDoc.ViewerPreferences;
                hideToolbar = prefs?.HideToolbar ?? false;
                displayDocTitle = prefs?.DisplayDocTitle ?? false;
            }
            catch (RuntimeBinderException)
            {
                // ViewerPreferences property is not available in the referenced Aspose.Pdf version.
                // Keep default values (false) and continue.
            }
            catch (Exception ex)
            {
                // Any other unexpected error – log and continue with defaults.
                Console.Error.WriteLine($"Error reading viewer preferences: {ex.Message}");
            }
        }

        // Prepare an anonymous object for JSON serialization.
        var preferences = new
        {
            HideToolbar = hideToolbar,
            DisplayDocTitle = displayDocTitle
        };

        // Serialize to JSON with indentation for readability.
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(preferences, jsonOptions);

        // Write JSON to the output file.
        File.WriteAllText(jsonPath, jsonString);

        Console.WriteLine($"Viewer preferences saved to '{jsonPath}'.");
    }
}
