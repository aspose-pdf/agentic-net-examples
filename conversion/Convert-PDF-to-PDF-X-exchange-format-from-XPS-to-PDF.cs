using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Facades;      // For ConvertErrorAction enum (if not in core)

class Program
{
    static void Main()
    {
        // Paths for source XPS, optional intermediate PDF, conversion log and final PDF/X output
        const string xpsPath   = "input.xps";
        const string pdfPath   = "intermediate.pdf";   // optional, can be omitted
        const string logPath   = "conversion_log.xml";
        const string pdfxPath  = "output_pdfx3.pdf";

        // Verify source file exists
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"Source XPS file not found: {xpsPath}");
            return;
        }

        try
        {
            // Load the XPS file using XpsLoadOptions – this creates a PDF document in memory
            using (Document doc = new Document(xpsPath, new XpsLoadOptions()))
            {
                // OPTIONAL: save the intermediate PDF (useful for inspection)
                doc.Save(pdfPath);

                // Convert the in‑memory PDF to PDF/X‑3 format.
                // The conversion log will be written to logPath.
                // ConvertErrorAction.Delete tells the converter to drop objects it cannot convert.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/X‑3 document.
                doc.Save(pdfxPath);
            }

            Console.WriteLine($"Conversion completed successfully.");
            Console.WriteLine($"Intermediate PDF: {pdfPath}");
            Console.WriteLine($"PDF/X‑3 output : {pdfxPath}");
            Console.WriteLine($"Log file       : {logPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}