using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize PPTX save options (required for PDF → PPTX conversion)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the PDF as a PowerPoint file.
            // Aspose.Pdf automatically creates a notes page for each slide when possible.
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}