using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPdfAPath = "output_pdfa.pdf";
        const string outputPdfXPath = "output_pdfx.pdf";
        const string logPath        = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // ---------- Convert to PDF/A ----------
                // Convert to PDF/A-1B, logging conversion errors to a text file.
                // The Convert method returns a bool indicating success.
                bool pdfaResult = doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                Console.WriteLine($"PDF/A conversion success: {pdfaResult}");

                // Save the converted document as a regular PDF file.
                doc.Save(outputPdfAPath);
                Console.WriteLine($"PDF/A saved to '{outputPdfAPath}'");

                // ---------- Convert to PDF/X ----------
                // Re‑use the same Document instance (now in PDF/A form) and convert to PDF/X-3.
                bool pdfxResult = doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
                Console.WriteLine($"PDF/X conversion success: {pdfxResult}");

                // Save the PDF/X result.
                doc.Save(outputPdfXPath);
                Console.WriteLine($"PDF/X saved to '{outputPdfXPath}'");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}