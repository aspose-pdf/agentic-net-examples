using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable auto‑tagging globally
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Register a simple font substitution (fallback to Arial for any missing font)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑4, write conversion log, and delete problematic objects
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

                // Save the converted PDF/A‑4 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑4 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
