using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for JavascriptAction

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // NOTE: ViewerPreferences property is not available in the current Aspose.Pdf version.
            // The desired UI hiding is achieved via JavaScript set as the document's OpenAction.

            // Add JavaScript that hides the toolbar and menu bar when the document opens
            string js = "app.viewerPreferences.toolbar = false; app.viewerPreferences.menubar = false;";
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden UI to '{outputPath}'.");
    }
}
