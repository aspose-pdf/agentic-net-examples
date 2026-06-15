using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_alert.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a JavaScript action that shows an alert dialog
            JavascriptAction jsAction = new JavascriptAction("app.alert('Document opened!');");

            // Assign the action to be executed when the document is opened
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with open-action JavaScript: {outputPath}");
    }
}