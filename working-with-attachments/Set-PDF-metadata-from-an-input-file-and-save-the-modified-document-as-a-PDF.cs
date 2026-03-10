using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set desired metadata fields via the DocumentInfo object (doc.Info)
            doc.Info.Title        = "Sample Document Title";
            doc.Info.Author       = "John Doe";
            doc.Info.Subject      = "Demonstration of setting PDF metadata";
            doc.Info.Keywords     = "Aspose.Pdf, metadata, example";
            doc.Info.Creator      = "My Application";
            doc.Info.Producer     = "Aspose.Pdf for .NET";
            doc.Info.ModDate      = DateTime.Now;
            doc.Info.CreationDate = DateTime.Now.AddDays(-1);

            // Save the modified document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}