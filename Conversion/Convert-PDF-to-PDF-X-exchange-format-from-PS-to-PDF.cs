using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psInputPath   = "input.ps";          // PostScript source
        const string pdfOutputPath = "intermediate.pdf"; // Optional intermediate PDF
        const string pdfxPath      = "output_pdfx3.pdf"; // PDF/X-3 result
        const string logPath       = "conversion_log.txt";

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        // Load the PS file using the input‑only PsLoadOptions
        using (Document doc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Optional: save as a regular PDF first
            doc.Save(pdfOutputPath);

            // Convert the document to PDF/X‑3, logging any conversion issues
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the PDF/X‑3 compliant document
            doc.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X saved to '{pdfxPath}'. Log written to '{logPath}'.");
    }
}