using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes PptxSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (preserves master slide layout by default)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX using the options (lifecycle rule: use Document.Save with SaveOptions)
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}