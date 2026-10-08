using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to inspect its XMP metadata. The Metadata dictionary is populated only when XMP is present.
        using (Document doc = new Document(inputPath))
        {
            bool hasXmp = doc.Metadata != null && doc.Metadata.Count > 0;

            if (!hasXmp)
            {
                Console.WriteLine("No XMP metadata found. Skipping modifications.");
                return;
            }

            Console.WriteLine("XMP metadata detected. Proceeding with modifications.");

            // Add a custom document property using the DocumentInfo indexer (the correct way to store custom metadata).
            doc.Info["ProcessedOn"] = DateTime.UtcNow.ToString("o");

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modifications saved to '{outputPath}'.");
    }
}
