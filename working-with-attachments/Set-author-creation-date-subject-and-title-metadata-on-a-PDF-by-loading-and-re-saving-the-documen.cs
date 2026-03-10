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

        // Load the PDF, modify its metadata, and save it.
        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata fields.
            doc.Info.Author       = "John Doe";
            doc.Info.Title        = "Sample Document";
            doc.Info.Subject      = "Demonstration of metadata setting";
            doc.Info.CreationDate = DateTime.Now;

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}