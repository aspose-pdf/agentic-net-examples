using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string outputPdfAPath = "output_pdfa.pdf";
        const string conversionLog = "conversion.log";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ---------- Step 1: Load the source PDF ----------
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // ---------- Step 2: Convert PDF to DOCX ----------
                var docSaveOptions = new DocSaveOptions
                {
                    // Export as DOCX format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use full flow recognition for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: improve bullet detection
                    RecognizeBullets = true
                };

                pdfDocument.Save(outputDocxPath, docSaveOptions);
                Console.WriteLine($"PDF converted to DOCX: {outputDocxPath}");
            }

            // ---------- Step 3: Load the generated DOCX ----------
            using (Document docxDocument = new Document(outputDocxPath))
            {
                // ---------- Step 4: Convert DOCX to PDF/A ----------
                // Convert to PDF/A-1B, logging any conversion errors
                docxDocument.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A document
                docxDocument.Save(outputPdfAPath);
                Console.WriteLine($"DOCX converted to PDF/A: {outputPdfAPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
