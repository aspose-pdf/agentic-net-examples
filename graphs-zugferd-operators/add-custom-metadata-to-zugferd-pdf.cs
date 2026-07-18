using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_zugferd.pdf";
        const string outputPath = "output_zugferd.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add custom metadata entries
            doc.Metadata.Add("ProjectCode", "PRJ-2023-001");
            doc.Metadata.Add("Department", "Finance");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom metadata added and saved to '{outputPath}'.");
    }
}