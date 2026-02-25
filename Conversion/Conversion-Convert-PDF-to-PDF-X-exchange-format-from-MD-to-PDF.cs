using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mdPath        = "input.md";          // source Markdown file
        const string pdfPath       = "intermediate.pdf";  // temporary PDF after MD conversion
        const string pdfxPath      = "output_pdfx3.pdf";  // final PDF/X‑3 file
        const string conversionLog = "conversion_log.txt";

        // Verify source file exists
        if (!File.Exists(mdPath))
        {
            Console.Error.WriteLine($"Source file not found: {mdPath}");
            return;
        }

        // ---------- Step 1: Load Markdown and save as PDF ----------
        // MdLoadOptions tells Aspose.Pdf how to interpret the .md file.
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        using (Document mdDoc = new Document(mdPath, mdLoadOptions))
        {
            // Save the intermediate PDF (optional, but useful for inspection)
            mdDoc.Save(pdfPath);
        }

        // ---------- Step 2: Convert the PDF to PDF/X‑3 ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Convert to PDF/X‑3, logging any conversion issues.
            // The Convert method returns a bool indicating success.
            bool conversionResult = pdfDoc.Convert(
                conversionLog,               // path to log file
                PdfFormat.PDF_X_3,           // target format
                ConvertErrorAction.Delete); // drop objects that cannot be converted

            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion to PDF/X‑3 reported errors. See log for details.");
            }

            // Save the resulting PDF/X‑3 document.
            pdfDoc.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X‑3 saved to '{pdfxPath}'.");
    }
}