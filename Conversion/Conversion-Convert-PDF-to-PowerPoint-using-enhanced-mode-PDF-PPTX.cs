using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document and PptxSaveOptions)

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
            // Initialize save options for PPTX (enhanced conversion mode is the default behavior)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the PDF as a PPTX file using the specified options
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}