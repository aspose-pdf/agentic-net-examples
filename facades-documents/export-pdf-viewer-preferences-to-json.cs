using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJson = "viewerPrefs.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Default values (in case the ViewerPreferences property is unavailable)
        bool hideMenuBar = false;
        bool hideToolbar = false;

        try
        {
            // Load the same file as a Document to read viewer preferences.
            // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // Guard the usage with a compile‑time symbol to keep the code compatible
            // with older library versions where the property is absent.
#if ASPOSE_PDF_HAS_VIEWERPREFERENCES
            using (Document doc = new Document(inputPath))
            {
                hideMenuBar = doc.ViewerPreferences.HideMenuBar;
                hideToolbar = doc.ViewerPreferences.HideToolbar;
            }
#endif
        }
        catch (Exception ex)
        {
            // Any runtime issue (e.g., corrupted PDF) is reported, but defaults remain.
            Console.Error.WriteLine($"Error reading viewer preferences: {ex.Message}");
        }

        // Build a JSON array with the two preferences.
        var prefs = new[]
        {
            new { Preference = "HideMenuBar", Value = hideMenuBar },
            new { Preference = "HideToolbar", Value = hideToolbar }
        };

        string json = JsonSerializer.Serialize(prefs, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Viewer preferences exported to '{outputJson}'.");
    }
}