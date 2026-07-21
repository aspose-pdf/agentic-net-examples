using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string logPath = "conversion_log.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/X‑3 compliance.
            // The conversion writes a log file (optional) and preserves embedded color profiles.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully exported as PDF/X‑3 to '{outputPath}'.");
    }
}