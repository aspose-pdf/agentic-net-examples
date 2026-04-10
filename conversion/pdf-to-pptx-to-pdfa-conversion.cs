using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to be converted
        const string inputPdfPath = "input.pdf";

        // Intermediate PPTX file path
        const string intermediatePptxPath = "intermediate.pptx";

        // Final PDF/A file path
        const string outputPdfAPath = "output_pdfa.pdf";

        // Log file for conversion diagnostics
        const string conversionLogPath = "conversion.log";

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
                // Initialize PPTX save options (default constructor)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX using explicit SaveOptions
                pdfDocument.Save(intermediatePptxPath, pptxOptions);
            }

            // -------------------------------------------------
            // Step 2: Convert PPTX -> PDF/A
            // -------------------------------------------------
            using (Document pptxDocument = new Document(intermediatePptxPath))
            {
                // Convert the document to PDF/A (PDF_A_1B) and log any conversion issues
                pptxDocument.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A document. The Convert call already produces a PDF/A compliant file,
                // so no additional PdfSaveOptions are required.
                pptxDocument.Save(outputPdfAPath);
            }

            Console.WriteLine($"Conversion completed successfully.\nPPTX: {intermediatePptxPath}\nPDF/A: {outputPdfAPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
