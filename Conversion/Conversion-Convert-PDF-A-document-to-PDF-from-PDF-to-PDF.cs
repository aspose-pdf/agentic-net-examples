using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_pdfa.pdf";   // PDF/A source file
        const string outputPath = "output.pdf";       // Regular PDF output
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Optional: log any conversion issues (e.g., PUA symbols) while converting
                // Here we simply convert to the default PDF format (PdfFormat.PDF_A_1B is for PDF/A;
                // using the overload without specifying a format converts to regular PDF)
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Remove PDF/A compliance flags to produce a plain PDF
                doc.RemovePdfaCompliance();

                // Save the resulting plain PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}