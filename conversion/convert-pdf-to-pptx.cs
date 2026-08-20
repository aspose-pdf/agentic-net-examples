using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes Document, SaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize default PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the PDF as an editable PPTX file using the options
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}