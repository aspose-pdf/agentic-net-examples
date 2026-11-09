using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        Document pdf = new Document(inputPath);

        // Add custom XMP metadata field "Department" with value "Finance"
        // Use the DocumentInfo indexer to store custom metadata; this updates the XMP packet
        // and makes the entry visible in the PDF properties dialog.
        pdf.Info["Department"] = "Finance";

        // Save the PDF with updated metadata
        pdf.Save(outputPath);

        Console.WriteLine($"XMP metadata added. Saved to '{outputPath}'.");
    }
}
