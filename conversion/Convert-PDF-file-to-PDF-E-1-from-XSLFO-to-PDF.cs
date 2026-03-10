using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        string xslFoPath = Path.Combine(dataDir, "input.xslfo");   // XSL‑FO source
        string pdfPath   = Path.Combine(dataDir, "output.pdf");   // PDF/E‑1 result
        string logPath   = Path.Combine(dataDir, "conversion.log"); // optional log

        // Verify source file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load XSL‑FO using XslFoLoadOptions
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Create Document from XSL‑FO
        using (Document doc = new Document(xslFoPath, loadOptions))
        {
            // Convert to PDF/E‑1 format.
            // PdfFormat.PDF_E_1 is the enum value for PDF/E‑1.
            // ConvertErrorAction.Delete skips objects that cannot be converted.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document as PDF/E‑1.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E‑1 saved to: {pdfPath}");
    }
}