using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        const string ofdPath      = "input.ofd";          // source OFD file
        const string pdfPath      = "output.pdf";         // target PDF/E‑1 file
        const string logPath      = "conversion.log";     // optional conversion log

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"Source file not found: {ofdPath}");
            return;
        }

        // Load the OFD file and convert it to PDF/E‑1
        using (Document doc = new Document())
        {
            // Load OFD using the appropriate load options
            doc.LoadFrom(ofdPath, new OfdLoadOptions());

            // Convert to PDF/E‑1 format.
            // PdfFormat.PDF_E_1 is the enum value for PDF/E‑1 (available in Aspose.Pdf).
            // ConvertErrorAction.Delete tells the converter to delete objects that cannot be converted.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the resulting PDF/E‑1 document.
            // No SaveOptions are needed because the target format is PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E‑1 saved to '{pdfPath}'.");
    }
}