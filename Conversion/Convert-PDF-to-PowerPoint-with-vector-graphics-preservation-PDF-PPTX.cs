using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and convert to PPTX
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize save options for PPTX format
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Save the document as PPTX preserving vector graphics
            pdfDocument.Save(outputPptxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}