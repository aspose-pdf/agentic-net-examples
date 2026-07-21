using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Intermediate DOCX file
        const string intermediateDocxPath = "intermediate.docx";

        // Final PDF/A compliant file
        const string outputPdfAPath = "output_pdfa.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF -> DOCX
        // -------------------------------------------------
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions docSaveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use full text flow recognition for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the DOCX file
            pdfDocument.Save(intermediateDocxPath, docSaveOptions);
        }

        // -------------------------------------------------
        // Step 2: Load DOCX and convert to PDF/A
        // -------------------------------------------------
        if (!File.Exists(intermediateDocxPath))
        {
            Console.Error.WriteLine($"Intermediate DOCX not found: {intermediateDocxPath}");
            return;
        }

        using (Document docxDocument = new Document(intermediateDocxPath))
        {
            // Convert the document to PDF/A-1B.
            // The conversion logs are written to a text file (optional).
            const string conversionLogPath = "conversion_log.txt";
            docxDocument.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A compliant document
            docxDocument.Save(outputPdfAPath);
        }

        Console.WriteLine($"Conversion completed. PDF/A saved to '{outputPdfAPath}'.");
    }
}