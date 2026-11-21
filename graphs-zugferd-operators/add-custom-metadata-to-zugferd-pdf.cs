using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input_zugferd.pdf";
        const string outputPath = "output_zugferd_with_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing ZUGFeRD PDF
        using (Document doc = new Document(inputPath))
        {
            // Add custom metadata entries using the DocumentInfo indexer
            doc.Info["ProjectCode"] = "PRJ-2023-001";
            doc.Info["Department"] = "Finance";

            // Optionally update the modification date to reflect the change
            doc.Info.ModDate = DateTime.Now;

            // Save the PDF; the ZUGFeRD attachment and existing structure are preserved.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}
