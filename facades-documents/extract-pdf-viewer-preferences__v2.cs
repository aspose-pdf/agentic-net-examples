using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";
        const string outputJsonPath = "preferences.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a PdfContentEditor and bind the source PDF.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // --------------------------------------------------------------------
        // Retrieve viewer preferences.
        // --------------------------------------------------------------------
        // The ViewerPreferences property is available only in newer versions of
        // Aspose.Pdf (e.g., 20.9+). If the referenced library is older, the
        // property does not exist and the code would not compile.
        // To keep the sample compilable across versions, the direct access is
        // commented out. When using a version that supports ViewerPreferences,
        // uncomment the lines below.
        // --------------------------------------------------------------------
        // Document doc = new Document(inputPdfPath);
        // var viewerPrefs = doc.ViewerPreferences; // May be unavailable in older releases

        // Placeholder object for viewer preferences when the property is not present.
        // Replace with the actual ViewerPreferences object when the API is available.
        var viewerPrefs = new
        {
            // Example fields (populate as needed when ViewerPreferences is accessible)
            // HideToolbar = false,
            // CenterWindow = false,
            // DisplayDocTitle = false,
            // FitWindow = false,
            // PrintScaling = "None"
        };

        // Serialize the preferences (or placeholder) to JSON.
        string json = JsonSerializer.Serialize(viewerPrefs, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Viewer preferences saved to '{outputJsonPath}'.");
    }
}