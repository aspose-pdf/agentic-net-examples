using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input_pdfa3b.pdf";   // PDF/A‑3b source
        const string outputPath = "output_regular.pdf"; // regular PDF result
        const string logPath = "conversion_log.xml";   // conversion log (required)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF/A‑3b document and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to a non‑PDF/A PDF by targeting a standard PDF version.
            // PdfFormat.PDF does not exist; use a specific PDF version enum value instead.
            doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

            // Save the resulting regular PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Converted PDF/A‑3b to regular PDF: {outputPath}");
    }
}
