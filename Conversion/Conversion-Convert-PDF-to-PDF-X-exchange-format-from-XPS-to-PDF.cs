using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XPS file, intermediate PDF, output PDF/X file and conversion log
        const string xpsPath      = "input.xps";
        const string pdfPath      = "intermediate.pdf";
        const string pdfxPath     = "output_pdfx3.pdf";
        const string conversionLog = "conversion_log.txt";

        // Verify input file exists
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load XPS and save as PDF
            // ------------------------------------------------------------
            XpsLoadOptions xpsLoadOptions = new XpsLoadOptions();

            using (Document pdfDoc = new Document(xpsPath, xpsLoadOptions))
            {
                // Save the intermediate PDF
                pdfDoc.Save(pdfPath);
            }

            // ------------------------------------------------------------
            // 2. Load the intermediate PDF and convert to PDF/X‑3
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Convert to PDF/X‑3, logging any conversion issues
                pdfDoc.Convert(conversionLog, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the final PDF/X‑3 file
                pdfDoc.Save(pdfxPath);
            }

            Console.WriteLine($"Conversion completed successfully:");
            Console.WriteLine($"  XPS  → PDF      : {pdfPath}");
            Console.WriteLine($"  PDF  → PDF/X‑3 : {pdfxPath}");
            Console.WriteLine($"  Log  → {conversionLog}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}