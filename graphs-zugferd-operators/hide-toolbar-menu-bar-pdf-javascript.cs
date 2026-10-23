using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Attempt to set viewer preferences (HideToolbar, HideMenubar).
            // The ViewerPreferences property is available only in newer Aspose.Pdf versions.
            // Use dynamic access with a try/catch to keep the code compatible with older versions.
            dynamic dynDoc = doc;
            try
            {
                dynDoc.ViewerPreferences.HideToolbar = true;
                dynDoc.ViewerPreferences.HideMenubar = true;
            }
            catch (RuntimeBinderException)
            {
                // ViewerPreferences not supported – silently ignore or log if needed.
                Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf version.");
            }

            // Embed JavaScript that runs when the document is opened.
            // This works on viewers that support JavaScript (e.g., Adobe Acrobat).
            doc.OpenAction = new JavascriptAction("app.toolbar = false; app.menu = false;");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden toolbar and menu bar: {outputPath}");
    }
}
