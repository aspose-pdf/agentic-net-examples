using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath       = "input.cgm";
        const string outputPdfPath = "output_pdfe1.pdf";
        const string logPath       = "conversion_log.txt";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file using CGM load options (CGM is input‑only)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        using (Document doc = new Document())
        {
            // LoadFrom converts the CGM into an internal PDF representation
            doc.LoadFrom(cgmPath, loadOptions);

            // Convert the document to PDF/E‑1 format.
            // ConvertErrorAction.Delete removes objects that cannot be converted.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the resulting PDF/E‑1 file.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E‑1 saved to '{outputPdfPath}'.");
    }
}