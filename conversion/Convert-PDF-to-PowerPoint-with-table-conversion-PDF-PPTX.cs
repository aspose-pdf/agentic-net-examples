using System;
using System.IO;
using Aspose.Pdf;               // Document, PptxSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize PPTX save options.
            // OptimizeTextBoxes helps the converter recognize tables and text columns.
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                OptimizeTextBoxes = true
            };

            // Save the PDF as a PowerPoint presentation using the explicit save options.
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}