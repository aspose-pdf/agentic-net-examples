using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string ofdPath      = "input.ofd";
        const string pdfPath      = "output.pdf";
        const string conversionLog = "conversion.log";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load OFD file (input format) using OfdLoadOptions
        using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Convert the document to PDF/X‑3 format.
            // The conversion log will contain any conversion warnings/errors.
            doc.Convert(conversionLog, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X‑3 document.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X saved to '{pdfPath}'.");
    }
}