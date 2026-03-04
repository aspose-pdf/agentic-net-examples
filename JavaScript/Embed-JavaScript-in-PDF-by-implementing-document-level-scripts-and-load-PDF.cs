using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction resides here

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF with embedded JavaScript
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF, embed a document-level JavaScript action, and save
        using (Document doc = new Document(inputPath))
        {
            // Create a JavaScript action that runs when the document is opened
            JavascriptAction jsAction = new JavascriptAction("app.alert('Document opened');");

            // Assign the action as the document's OpenAction (document-level script)
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{outputPath}'.");
    }
}