using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to hide toolbar and menu bar when the document opens
            string js = "app.toolbar = false; app.menuBar = false;";

            // Assign the JavaScript as the document's open action
            doc.OpenAction = new JavascriptAction(js);

            // Also set the viewer preference flags directly (optional but reinforces the intent)
            doc.HideToolBar = true;
            doc.HideMenubar = true;

            // Save the modified PDF (using lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden UI to '{outputPath}'.");
    }
}