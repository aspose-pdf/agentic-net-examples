using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input_pdfa3a.pdf";   // PDF/A‑3a source
        const string outputPath = "output_regular.pdf"; // regular PDF output
        const string logPath = "conversion_log.xml";   // optional conversion log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF/A‑3a document and convert it to a regular PDF.
        // Use Document.Convert with a standard PDF version (e.g., v_1_7) to strip PDF/A compliance.
        using (Document doc = new Document(inputPath))
        {
            // Convert to a standard PDF (non‑PDF/A) and delete any conversion errors.
            doc.Convert(logPath, PdfFormat.v_1_7, ConvertErrorAction.Delete);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Converted PDF/A‑3a to regular PDF: {outputPath}");
    }
}
