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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that saves the document every 60 seconds (60000 ms)
            string js = "app.setInterval('this.saveAs({cPath:\"output_with_js.pdf\"});', 60000);";

            // Attach the script as a document‑level action executed after saving
            // (this ensures the script is embedded in the PDF)
            doc.Actions.AfterSaving = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑save JavaScript saved to '{outputPath}'.");
    }
}