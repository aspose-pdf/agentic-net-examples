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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript open action: jump to page 5 (zero‑based index 4)
            doc.OpenAction = new JavascriptAction("this.pageNum = 4;");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with open action saved to '{outputPath}'.");
    }
}