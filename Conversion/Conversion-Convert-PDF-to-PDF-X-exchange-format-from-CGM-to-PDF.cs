using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file, intermediate PDF, final PDF/X-3 file and conversion log
        const string cgmPath   = "input.cgm";
        const string pdfPath   = "intermediate.pdf";
        const string pdfxPath  = "output_pdfx3.pdf";
        const string logPath   = "conversion_log.xml";

        // Verify the CGM source exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Use a single Document instance inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Load the CGM file – CGM is input‑only, so we use CgmLoadOptions
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            doc.LoadFrom(cgmPath, loadOptions);

            // Save the loaded content as a regular PDF (intermediate step)
            doc.Save(pdfPath);

            // Convert the PDF to PDF/X‑3 format.
            // The Convert method writes conversion messages to the specified log file.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X‑3 document
            doc.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X‑3 saved to '{pdfxPath}'.");
    }
}