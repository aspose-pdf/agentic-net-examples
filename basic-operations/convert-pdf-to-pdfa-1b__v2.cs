using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath = "conversion_log.xml"; // optional conversion log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑1b compliance.
            // ConvertErrorAction.Delete removes objects that prevent successful conversion.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the converted document. No special SaveOptions are required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
