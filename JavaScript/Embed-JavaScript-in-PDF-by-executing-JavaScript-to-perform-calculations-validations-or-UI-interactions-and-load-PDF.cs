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

        // Load the existing PDF, add a document‑level JavaScript action, and save.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that will be executed when the PDF is opened.
            // Example: show an alert dialog.
            JavascriptAction openJs = new JavascriptAction("app.alert('Welcome! This PDF contains embedded JavaScript.');");

            // Assign the script to the document's OpenAction so it runs on load.
            doc.OpenAction = openJs;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{outputPath}'.");
    }
}