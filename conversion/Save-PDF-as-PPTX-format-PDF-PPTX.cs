using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions

class Program
{
    static void Main()
    {
        // Paths for input PDF and output PPTX
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, convert and save as PPTX using explicit save options
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize PPTX save options (required to produce PPTX output)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the document as PPTX
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}