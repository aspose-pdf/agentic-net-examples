using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Required for RuntimeBinderException when using dynamic

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputYaml = "viewer_preferences.yaml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // ViewerPreferences were added in later Aspose.Pdf versions. To keep the code
            // compatible with older versions we access the property via dynamic and handle
            // the possible RuntimeBinderException.
            bool centerWindow = false;
            bool fitWindow = false;
            bool displayDocTitle = false;

            dynamic docDyn = pdfDocument;
            try
            {
                var prefs = docDyn.ViewerPreferences; // runtime access
                centerWindow = prefs.CenterWindow;
                fitWindow = prefs.FitWindow;
                displayDocTitle = prefs.DisplayDocTitle;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Defaulting flags to false.");
            }

            // Build a simple YAML representation (lower‑case booleans for readability)
            string yamlContent =
                $"CenterWindow: {centerWindow.ToString().ToLower()}\n" +
                $"FitWindow: {fitWindow.ToString().ToLower()}\n" +
                $"DisplayDocTitle: {displayDocTitle.ToString().ToLower()}\n";

            // Write YAML to the output file
            File.WriteAllText(outputYaml, yamlContent);

            Console.WriteLine($"Viewer preferences extracted to '{outputYaml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
