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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑1b compliance.
            // ConvertErrorAction.Delete removes objects that prevent compliance.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the converted document. Missing fonts are embedded automatically during conversion.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
