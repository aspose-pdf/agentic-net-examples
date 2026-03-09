using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document, process it, and ensure resources are released.
        using (Document doc = new Document(inputPath))
        {
            // Remove all interactive form fields, placing their current values onto the page.
            doc.Flatten();

            // Save the resulting static PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields stripped. Output saved to '{outputPath}'.");
    }
}