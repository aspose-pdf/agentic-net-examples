using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired SVG output file path (extension determines format)
        const string outputPath = "output.svg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Save the document as SVG (the .svg extension triggers SVG format)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Conversion completed successfully. SVG saved to: {outputPath}");
    }
}