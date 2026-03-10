using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.ps";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PostScript file using PsLoadOptions (PS is input‑only)
        using (Document doc = new Document(inputPath, new PsLoadOptions()))
        {
            // Save the document as PDF (default format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Converted PS to PDF: '{outputPath}'");
    }
}