using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // PDF with updated info

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Edit document metadata via the DocumentInfo object
            doc.Info.Title  = "New Document Title";
            doc.Info.Author = "John Doe";
            doc.Info.Subject = "Updated subject line";
            doc.Info.Keywords = "Aspose, PDF, Metadata";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}