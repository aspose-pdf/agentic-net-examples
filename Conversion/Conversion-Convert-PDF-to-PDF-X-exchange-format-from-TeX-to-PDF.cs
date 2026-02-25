using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory that contains the source TeX file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input TeX file.
        string texPath = Path.Combine(dataDir, "input.tex");

        // Output PDF/X-3 file.
        string pdfxPath = Path.Combine(dataDir, "output_pdfx3.pdf");

        // Log file for conversion messages.
        string logPath = Path.Combine(dataDir, "conversion_log.txt");

        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX source not found: {texPath}");
            return;
        }

        // Load the TeX file using TeXLoadOptions.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Document implements IDisposable – wrap in using for deterministic disposal.
        using (Document doc = new Document(texPath, texLoadOptions))
        {
            // Convert the document to PDF/X‑3 format.
            // The Convert method writes conversion messages to the log file.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted PDF/X‑3 document.
            doc.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X‑3 saved to '{pdfxPath}'.");
    }
}