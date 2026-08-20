using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa2b.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, convert to PDF/A‑2b, then save.
        using (Document doc = new Document(inputPath))
        {
            // Convert to PDF/A‑2b; errors are logged to the specified file.
            doc.Convert(logPath, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑2b compliant file saved to '{outputPath}'.");
    }
}