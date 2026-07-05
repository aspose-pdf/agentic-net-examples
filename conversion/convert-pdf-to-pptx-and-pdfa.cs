using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for potential future extensions (optional)

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string intermediatePptxPath = "intermediate.pptx"; // PPTX output
        const string outputPdfAPath = "archival_output.pdf";   // final PDF/A
        const string conversionLog  = "conversion.log";      // log for PDF/A conversion

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Step 1: Convert PDF to PPTX using PptxSaveOptions
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (default constructor)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX
                pdfDoc.Save(intermediatePptxPath, pptxOptions);
                Console.WriteLine($"PDF converted to PPTX: {intermediatePptxPath}");
            }

            // ------------------------------------------------------------
            // Step 2: Load the generated PPTX and convert it to PDF/A
            // ------------------------------------------------------------
            using (Document pptxDoc = new Document())
            {
                // Load the PPTX file. Passing null for LoadOptions uses default handling.
                pptxDoc.LoadFrom(intermediatePptxPath, null);

                // Convert the document to PDF/A (PDF_A_1B) and log any conversion issues.
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                pptxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A file.
                pptxDoc.Save(outputPdfAPath);
                Console.WriteLine($"PPTX converted to PDF/A: {outputPdfAPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}