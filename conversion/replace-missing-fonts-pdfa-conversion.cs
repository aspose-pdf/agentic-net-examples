using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Register a fallback for any missing font (e.g., Arial)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        using (Document doc = new Document(inputPath))
        {
            // Convert to PDF/A‑1b, write conversion log, delete problematic objects
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            // Save the resulting PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A conversion completed. Output saved to '{outputPath}'.");
    }
}
