using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document and save it as a new PDF (basic copy/conversion)
        using (Document doc = new Document(inputPath))
        {
            // Save creates a copy of the PDF; no additional options are required for PDF‑to‑PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF conversion completed. Output saved to '{outputPath}'.");
    }
}