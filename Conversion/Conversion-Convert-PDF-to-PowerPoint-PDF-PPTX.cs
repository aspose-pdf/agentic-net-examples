using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions are defined here

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination PPTX
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create PPTX save options (default settings are sufficient for most cases)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as a PPTX file using the specified options
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptx}'");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during conversion
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}