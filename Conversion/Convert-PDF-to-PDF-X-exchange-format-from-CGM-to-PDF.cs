using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";          // Source CGM file
        const string pdfPath = "intermediate.pdf";   // Optional intermediate PDF
        const string pdfxPath = "output_pdfx3.pdf";  // Target PDF/X‑3 file
        const string logPath = "conversion_log.txt"; // Log for conversion errors

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Load CGM using CgmLoadOptions and work within a using block for deterministic disposal
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Optional: save the plain PDF version (can be omitted if not needed)
            doc.Save(pdfPath);

            // Convert the document to PDF/X‑3 format, logging any conversion errors
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X document
            doc.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X saved to '{pdfxPath}'.");
    }
}