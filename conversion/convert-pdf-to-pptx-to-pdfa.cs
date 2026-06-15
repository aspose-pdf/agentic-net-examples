using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf classes (Document, PptxSaveOptions, PdfFormat, ConvertErrorAction, etc.) are in this namespace

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

        // Ensure the input file exists
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
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (required for non‑PDF output)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as a PPTX file
                pdfDocument.Save(intermediatePptxPath, pptxOptions);
            }

            // ------------------------------------------------------------
            // Step 2: Load the generated PPTX and convert it to PDF/A
            // ------------------------------------------------------------
            using (Document pptxDocument = new Document(intermediatePptxPath))
            {
                // Convert to PDF/A‑1B. The first argument is the output path – an empty string means the document stays in memory.
                pptxDocument.Convert(string.Empty, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A document to disk
                pptxDocument.Save(outputPdfAPath);
            }

            Console.WriteLine($"Conversion complete. PDF/A saved to '{outputPdfAPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
