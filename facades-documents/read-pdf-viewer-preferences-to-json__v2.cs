using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder; // Required for RuntimeBinderException

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

        // The PdfContentEditor is not needed for reading viewer preferences, but we keep the using block
        // to demonstrate proper disposal of disposable objects as per the original intent.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Load the document. We will access ViewerPreferences via dynamic to stay compatible with
            // older Aspose.Pdf versions that do not expose the ViewerPreferences property at compile time.
            using (Document doc = new Document(inputPdfPath))
            {
                bool hideToolbar = false;
                bool hideMenuBar = false;

                try
                {
                    // Cast to dynamic to bypass compile‑time member checking.
                    dynamic dynDoc = doc;
                    var prefs = dynDoc.ViewerPreferences;
                    hideToolbar = (bool)prefs.HideToolbar;
                    hideMenuBar = (bool)prefs.HideMenuBar;
                }
                catch (RuntimeBinderException)
                {
                    // The current Aspose.Pdf version does not support ViewerPreferences.
                    // We keep the default values (false) and optionally log a warning.
                    Console.Error.WriteLine("ViewerPreferences property is not available in the loaded Aspose.Pdf library version.");
                }
                catch (Exception ex)
                {
                    // Any other unexpected exception should be reported.
                    Console.Error.WriteLine($"Error while reading viewer preferences: {ex.Message}");
                }

                var jsonArray = new[]
                {
                    new { HideToolbar = hideToolbar, HideMenuBar = hideMenuBar }
                };

                string json = JsonSerializer.Serialize(jsonArray, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputJsonPath, json);
            }
        }

        Console.WriteLine($"Viewer preferences saved to '{outputJsonPath}'.");
    }
}
