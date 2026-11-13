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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑2b compliance.
                // The Convert method writes a log file and changes the document to the specified PDF format.
                doc.Convert(logPath, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

                // Save the converted document as a regular PDF file (it is now PDF/A‑2b compliant).
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑2b file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}