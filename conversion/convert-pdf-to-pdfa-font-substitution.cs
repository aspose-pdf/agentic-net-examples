using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Register a fallback font for any missing font (e.g., Arial)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑1b, write conversion log, delete pages that cause errors
                bool conversionOk = doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                if (!conversionOk)
                {
                    Console.Error.WriteLine("PDF/A conversion reported errors. See log for details.");
                }

                // Save the resulting PDF/A document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A conversion completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
