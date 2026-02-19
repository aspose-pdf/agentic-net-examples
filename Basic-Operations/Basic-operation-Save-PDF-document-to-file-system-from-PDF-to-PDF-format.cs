using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        string inputPath = "input.pdf";

        // Path where the PDF will be saved.
        string outputPath = "output.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document pdfDocument = new Document(inputPath);

        // Save the PDF document to the specified output path.
        pdfDocument.Save(outputPath);
    }
}