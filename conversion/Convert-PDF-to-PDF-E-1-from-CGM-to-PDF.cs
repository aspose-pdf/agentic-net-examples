using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file and output PDF/E-1 file paths
        const string cgmPath   = "input.cgm";
        const string pdfPath   = "output.pdf";          // PDF/E-1 output (saved as PDF)
        const string logPath   = "conversion_log.xml"; // optional conversion log

        // Verify input file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file using CgmLoadOptions (default A4 page size, 300 dpi)
            using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
            {
                // Convert the document to PDF/E‑1.
                // Aspose.Pdf represents PDF/E‑1 with the PDF/X‑3 format enum value.
                // The Convert method writes conversion details to a log file.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document as a PDF file (PDF/E‑1 compliant)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"CGM successfully converted to PDF/E-1: {pdfPath}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}