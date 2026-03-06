using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set its metadata, and save it back as PDF.
        using (Document doc = new Document(inputPath))
        {
            // File‑specific information (DocumentInfo)
            doc.Info.Author       = "John Doe";
            doc.Info.Title        = "Sample Document";
            doc.Info.Subject      = "Demonstration of setting PDF metadata";
            doc.Info.Keywords     = "Aspose.Pdf, metadata, example";
            doc.Info.Creator      = "My Application";
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate      = DateTime.Now;

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}