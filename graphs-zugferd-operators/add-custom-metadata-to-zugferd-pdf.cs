using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice_zugferd.pdf";
        const string outputPath = "invoice_zugferd_custom.pdf";

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

            // Save the PDF; the ZUGFeRD attachment remains intact
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}