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

        // Load the existing PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Attach a JavaScript action that runs when the document is opened.
            // The script shows an alert dialog with the specified message.
            doc.OpenAction = new JavascriptAction("app.alert('Document opened!');");

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
