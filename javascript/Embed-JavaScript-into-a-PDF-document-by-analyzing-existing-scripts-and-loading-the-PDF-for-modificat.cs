using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a JavaScript action that will be executed when the document is opened
            // Example script: show an alert dialog
            JavascriptAction jsAction = new JavascriptAction("app.alert('Document opened via Aspose.Pdf JavaScript action');");

            // Assign the JavaScript action to the document's OpenAction property
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{outputPath}'.");
    }
}