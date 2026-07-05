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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that hides the toolbar and menu bar when the document opens
            string js = "app.toolbar = false; app.menu = false;";

            // Add the script to the document's JavaScript collection using the indexer (named script)
            doc.JavaScript["HideUI"] = js;

            // Set the OpenAction to ensure the script runs when the document is opened
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden UI to '{outputPath}'.");
    }
}
