using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Intermediate DOCX file path
        const string docxPath = "intermediate.docx";
        // Output PDF/A file path
        const string pdfaPath = "output_pdfa.pdf";
        // Log file for conversion errors (optional)
        const string conversionLogPath = "conversion_log.txt";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF to DOCX
        // -------------------------------------------------
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options (non‑PDF format requires explicit options)
            DocSaveOptions docSaveOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use full flow recognition for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the document as DOCX
            pdfDocument.Save(docxPath, docSaveOptions);
        }

        // Verify the intermediate DOCX was created
        if (!File.Exists(docxPath))
        {
            Console.Error.WriteLine($"Failed to create DOCX file: {docxPath}");
            return;
        }

        // -------------------------------------------------
        // Step 2: Convert DOCX to PDF/A
        // -------------------------------------------------
        using (Document docxDocument = new Document(docxPath))
        {
            // Convert to PDF/A‑1B, logging any conversion issues
            docxDocument.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A document
            docxDocument.Save(pdfaPath);
        }

        Console.WriteLine($"Conversion completed. PDF/A saved to '{pdfaPath}'.");
    }
}