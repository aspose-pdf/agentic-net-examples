using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the target PPTX file.
        const string inputPath = "input.pdf";
        const string outputPath = "output.pptx";

        // Verify that the input PDF exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Save the document in PPTX format.
        // This uses the Document.Save method (lifecycle rule) with the PPTX format enum.
        pdfDocument.Save(outputPath, SaveFormat.Pptx);

        Console.WriteLine($"Conversion completed successfully. PPTX saved to: {outputPath}");
    }
}