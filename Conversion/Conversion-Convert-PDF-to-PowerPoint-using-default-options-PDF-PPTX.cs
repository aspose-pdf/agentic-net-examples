using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including PptxSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize default PPTX save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX using the explicit save options
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading or saving
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}