using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa2b.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑2b compliance.
            // ConvertErrorAction.Delete removes objects that cannot be converted.
            doc.Convert(logPath, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

            // Save the converted document. No SaveOptions needed because we are saving PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑2b compliant file saved to '{outputPath}'.");
    }
}