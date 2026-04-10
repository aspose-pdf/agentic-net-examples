using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF, add a JavaScript open action, and save.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to display an alert when the document is opened.
            JavascriptAction jsAction = new JavascriptAction("app.alert('Document opened!');");

            // Set the document's OpenAction to the JavaScript action.
            doc.OpenAction = jsAction;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript open action: {outputPath}");
    }
}