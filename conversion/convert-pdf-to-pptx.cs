using System;
using System.IO;
using Aspose.Pdf; // Provides Document, PptxSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination PPTX.
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify that the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize default PPTX save options.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the document as PPTX using the explicit save options.
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}