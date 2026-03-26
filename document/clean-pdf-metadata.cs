using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Remove all document metadata (including hidden metadata)
            doc.RemoveMetadata();

            // Optimize resources – this removes unused objects such as embedded files
            doc.OptimizeResources();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}