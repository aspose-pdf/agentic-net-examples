using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including PptxSaveOptions, are in this namespace

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
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize PPTX-specific save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX using the explicit save options
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}