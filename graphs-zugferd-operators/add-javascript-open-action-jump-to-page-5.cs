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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that sets the current page to page 5 (zero‑based index = 4)
            doc.OpenAction = new JavascriptAction("this.pageNum = 4;");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript open action to '{outputPath}'.");
    }
}
