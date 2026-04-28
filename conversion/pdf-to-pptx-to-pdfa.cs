using System;
using System.IO;
using Aspose.Pdf; // Core API: Document, PptxSaveOptions, PdfFormat, ConvertErrorAction

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Intermediate PPTX file
        const string intermediatePptxPath = "intermediate.pptx";

        // Final PDF/A file (archival version)
        const string outputPdfAPath = "archival_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // Step 1: Convert PDF -> PPTX
            // -------------------------------------------------
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (required for non‑PDF output)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX
                pdfDocument.Save(intermediatePptxPath, pptxOptions);
            }

            // -------------------------------------------------
            // Step 2: Convert PPTX -> PDF/A
            // -------------------------------------------------
            using (Document pptxDocument = new Document(intermediatePptxPath))
            {
                // Convert to PDF/A (PDF_A_1B) and log any conversion errors
                // The Convert method writes a log file; here we use "conversion.log"
                pptxDocument.Convert("conversion.log", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A document
                pptxDocument.Save(outputPdfAPath);
            }

            Console.WriteLine($"Conversion completed successfully.");
            Console.WriteLine($"PPTX file: {intermediatePptxPath}");
            Console.WriteLine($"PDF/A file: {outputPdfAPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}