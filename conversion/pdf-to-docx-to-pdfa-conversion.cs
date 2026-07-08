using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string outputPdfAPath = "output_pdfa.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ---------- Step 1: Load the source PDF ----------
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ---------- Step 2: Save as DOCX ----------
                DocSaveOptions docSaveOpts = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,   // Export to DOCX
                    Mode   = DocSaveOptions.RecognitionMode.Flow // Use full recognition for better editability
                };
                pdfDoc.Save(outputDocxPath, docSaveOpts);
                Console.WriteLine($"PDF converted to DOCX: {outputDocxPath}");
            }

            // ---------- Step 3: Load the generated DOCX ----------
            using (Document docxDoc = new Document(outputDocxPath))
            {
                // ---------- Step 4: Convert to PDF/A ----------
                // Convert to PDF/A-1B; errors are logged to a temporary file (can be ignored here)
                string conversionLog = Path.GetTempFileName();
                docxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // ---------- Step 5: Save the PDF/A compliant document ----------
                docxDoc.Save(outputPdfAPath); // No SaveOptions needed for PDF output
                Console.WriteLine($"DOCX converted to PDF/A: {outputPdfAPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
