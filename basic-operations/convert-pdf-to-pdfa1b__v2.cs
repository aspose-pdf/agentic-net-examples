using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // Source PDF
        const string outputPath = "output_pdfa1b.pdf"; // PDF/A‑1b result
        const string logPath = "conversion_log.xml";   // Optional conversion log

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using the recommended lifecycle pattern)
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑1b; missing fonts are embedded automatically.
                // ConvertErrorAction.Delete removes objects that prevent compliance.
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑1b: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
