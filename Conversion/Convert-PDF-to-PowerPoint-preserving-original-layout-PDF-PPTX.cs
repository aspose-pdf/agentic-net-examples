using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize PPTX save options (required for non‑PDF formats)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX, preserving the original layout
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}