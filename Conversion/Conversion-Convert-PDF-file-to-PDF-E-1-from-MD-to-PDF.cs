using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mdPath   = "input.md";          // source Markdown file
        const string pdfPath  = "temp.pdf";          // intermediate PDF
        const string pdfEPath = "output_pdfe1.pdf"; // final PDF/E‑1 file
        const string logPath  = "conversion_log.txt";

        if (!File.Exists(mdPath))
        {
            Console.Error.WriteLine($"Markdown file not found: {mdPath}");
            return;
        }

        // 1️⃣ Load the Markdown file and save it as a regular PDF.
        MdLoadOptions mdLoadOptions = new MdLoadOptions();
        using (Document mdDoc = new Document(mdPath, mdLoadOptions))
        {
            mdDoc.Save(pdfPath); // PDF output (no SaveOptions needed for PDF)
        }

        // 2️⃣ Open the intermediate PDF and convert it to PDF/E‑1.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Convert to PDF/E‑1, logging any conversion errors.
            pdfDoc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);
            // Save the converted document.
            pdfDoc.Save(pdfEPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E‑1 saved to '{pdfEPath}'.");
    }
}