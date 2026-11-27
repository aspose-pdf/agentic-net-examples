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
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "viewerPreferences.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Use PdfContentEditor (Aspose.Pdf.Facades) to load the PDF.
            // The editor gives access to the underlying Document instance.
            var editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Access ViewerPreferences via dynamic to stay compatible with older Aspose.Pdf versions.
            bool centerWindow = false;
            bool fitWindow = false;
            bool displayDocTitle = false;

            try
            {
                dynamic doc = editor.Document; // underlying Aspose.Pdf.Document
                var prefs = doc.ViewerPreferences;
                centerWindow = (bool)prefs.CenterWindow;
                fitWindow = (bool)prefs.FitWindow;
                displayDocTitle = (bool)prefs.DisplayDocTitle;
            }
            catch (RuntimeBinderException)
            {
                // ViewerPreferences property is not available in the referenced Aspose.Pdf version.
                // Keep default values (false) and inform the user.
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Defaulting to false.");
            }

            var preferences = new
            {
                CenterWindow = centerWindow,
                FitWindow = fitWindow,
                DisplayDocTitle = displayDocTitle
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(preferences, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Viewer preferences saved to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
