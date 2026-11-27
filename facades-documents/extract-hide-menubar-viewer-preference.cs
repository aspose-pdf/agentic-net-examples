using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

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

        try
        {
            // Load the PDF using PdfContentEditor (does not implement IDisposable)
            var editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Retrieve the viewer preferences mask (int) and check the HideMenubar flag.
            int prefMask = editor.GetViewerPreference();
            bool hideMenuBar = (prefMask & ViewerPreference.HideMenubar) != 0;

            // Prepare an object for JSON serialization
            var preferences = new
            {
                HideMenuBar = hideMenuBar
            };

            // Serialize to formatted JSON
            string json = JsonSerializer.Serialize(preferences, new JsonSerializerOptions { WriteIndented = true });

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
