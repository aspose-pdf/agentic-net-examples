using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source TeX file and where output files will be placed.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Paths for the source TeX, intermediate PDF, final PDF/X and conversion log.
        string texPath       = Path.Combine(dataDir, "input.tex");
        string intermediatePdf = Path.Combine(dataDir, "intermediate.pdf");
        string pdfxPath      = Path.Combine(dataDir, "output_pdfx3.pdf");
        string logPath       = Path.Combine(dataDir, "conversion_log.xml");

        // Verify the TeX source exists.
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load the TeX file and save it as a regular PDF.
        // ------------------------------------------------------------
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document texDoc = new Document(texPath, texLoadOptions))
        {
            // Save the intermediate PDF.
            texDoc.Save(intermediatePdf);
        }

        // ------------------------------------------------------------
        // Step 2: Load the intermediate PDF and convert it to PDF/X.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(intermediatePdf))
        {
            // Convert to PDF/X‑3 format. Errors are written to the log file.
            pdfDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X document.
            pdfDoc.Save(pdfxPath);
        }

        Console.WriteLine($"PDF/X file saved to '{pdfxPath}'.");
    }
}