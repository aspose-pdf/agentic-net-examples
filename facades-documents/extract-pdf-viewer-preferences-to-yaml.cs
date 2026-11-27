using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder; // for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string yamlOutputPath = "viewer_preferences.yaml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF with PdfContentEditor (facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // To keep the code compatible with older versions we access it via dynamic
            // and handle the possible RuntimeBinderException gracefully.
            bool fitWindow = false;
            bool displayDocTitle = false;

            dynamic doc = editor.Document; // use dynamic to bypass compile‑time checks
            try
            {
                var prefs = doc.ViewerPreferences;
                fitWindow = prefs.FitWindow;
                displayDocTitle = prefs.DisplayDocTitle;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Defaulting to false.");
            }

            // Build simple YAML content
            string yaml = $"fitWindow: {fitWindow.ToString().ToLowerInvariant()}{Environment.NewLine}" +
                          $"displayDocTitle: {displayDocTitle.ToString().ToLowerInvariant()}";

            // Save YAML configuration
            File.WriteAllText(yamlOutputPath, yaml);
        }

        Console.WriteLine($"Viewer preferences saved to '{yamlOutputPath}'.");
    }
}
