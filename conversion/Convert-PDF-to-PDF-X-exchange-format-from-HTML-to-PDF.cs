using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // For ConvertErrorAction enum (if needed)

class Program
{
    static void Main()
    {
        // Input HTML file and intermediate PDF file paths
        const string htmlPath   = "input.html";
        const string pdfPath    = "intermediate.pdf";
        const string pdfxPath   = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.txt";

        // Verify input file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert HTML to PDF
        // -----------------------------------------------------------------
        HtmlLoadOptions htmlLoadOptions = new HtmlLoadOptions(); // default options
        using (Document htmlDoc = new Document(htmlPath, htmlLoadOptions))
        {
            // Save the intermediate PDF (standard PDF format)
            htmlDoc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Convert the PDF to PDF/X-3 format
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Convert to PDF/X-3, logging any conversion errors
            pdfDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X-3 file
            pdfDoc.Save(pdfxPath);
        }

        Console.WriteLine($"HTML → PDF → PDF/X-3 conversion completed:");
        Console.WriteLine($"  PDF  : {pdfPath}");
        Console.WriteLine($"  PDF/X: {pdfxPath}");
        Console.WriteLine($"  Log  : {logPath}");
    }
}