using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zugferd.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Add custom metadata entries
            doc.Metadata.Add("ProjectCode", "PRJ-001");
            doc.Metadata.Add("Department", "Accounting");

            // Save the PDF with the new metadata
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}