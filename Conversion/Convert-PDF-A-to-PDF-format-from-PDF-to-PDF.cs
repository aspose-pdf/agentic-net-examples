using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_pdfa.pdf";   // PDF/A source file
        const string outputPath = "output.pdf";       // Regular PDF output

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A document (using the standard Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Remove PDF/A compliance flags – this converts the document to a regular PDF
                doc.RemovePdfaCompliance();

                // Save as a standard PDF (no special SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A successfully converted to PDF: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}