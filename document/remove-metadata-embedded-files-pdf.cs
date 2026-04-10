using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all metadata (including hidden metadata)
            doc.RemoveMetadata();

            // Remove all embedded files, if any
            // EmbeddedFiles.Delete() removes every embedded file from the document
            doc.EmbeddedFiles?.Delete();

            // Optimize resources to further reduce file size
            doc.OptimizeResources();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}