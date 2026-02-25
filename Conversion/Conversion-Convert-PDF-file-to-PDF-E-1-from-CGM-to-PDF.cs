using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";
        const string logPath = "conversion_log.txt";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load CGM using default options (A4 page size, 300 dpi)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Document must be disposed via using
        using (Document doc = new Document())
        {
            // Convert the CGM file into a PDF document
            doc.LoadFrom(cgmPath, loadOptions);

            // Convert the PDF to PDF/E‑1, logging any conversion issues
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the resulting PDF/E‑1 file (extension .pdf is fine)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed. Output saved to '{outputPdfPath}'.");
    }
}